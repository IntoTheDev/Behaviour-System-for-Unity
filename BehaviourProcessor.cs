using MEC;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using ToolBox.Behaviours.Actions;
using ToolBox.Behaviours.Composites;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : SerializedMonoBehaviour
	{
		[SerializeField, ListDrawerSettings(
			NumberOfItemsPerPage = 5,
			Expanded = true), TabGroup("Context")] private SharedData[] sharedDataset = null;

		[OdinSerialize, PageList, TabGroup("Behaviours")] private Behaviour[] behaviours = null;

#if UNITY_EDITOR
		[SerializeField, ReadOnly, TabGroup("Debug")] private string currentBehaviourName = "Behaviour";
		[SerializeField, ReadOnly, TabGroup("Debug")] private string previousBehaviourName = "";
#endif

		[SerializeField, ReadOnly, TabGroup("Debug")] private int currentIndex = 0;
		[SerializeField, ReadOnly, TabGroup("Debug")] private int previousIndex = -1;

		private Behaviour currentBehaviour = null;
		private Behaviour previousBehaviour = null;

		private bool isInitialized = false;

		private List<Task>[] tasks = null;
		private int[] tasksAmount = null;
		private CoroutineHandle[] tasksCoroutines = null;

		private const int tasksTotalCount = 4;

		private void Awake()
		{
			tasks = new List<Task>[tasksTotalCount];
			tasksCoroutines = new CoroutineHandle[tasksTotalCount];
			tasksAmount = new int[tasksTotalCount];

			int tasksInitialCapacity = 10;

			for (int i = 0; i < tasksTotalCount; i++)
				tasks[i] = new List<Task>(tasksInitialCapacity);

			if (behaviours.Length == 0 || behaviours[0] == null)
			{
				enabled = false;
				Debug.LogError("Behaviour is not valid", gameObject);
				return;
			}
		}

		private void Start()
		{
			for (int i = 0; i < behaviours.Length; i++)
				behaviours[i].Initialize(this);

			currentBehaviour = behaviours[0];
			currentBehaviour.OnEnter();

#if UNITY_EDITOR
			currentBehaviourName = currentBehaviour.Name;
#endif

			tasksCoroutines[(int)TaskSegment.Default] = Timing.RunCoroutine(Tick(), Segment.Update);
			tasksCoroutines[(int)TaskSegment.Slow] = Timing.RunCoroutine(SlowTick(), Segment.SlowUpdate);			
			tasksCoroutines[(int)TaskSegment.Fixed] = Timing.RunCoroutine(FixedTick(), Segment.FixedUpdate);
			tasksCoroutines[(int)TaskSegment.Late] = Timing.RunCoroutine(LateTick(), Segment.LateUpdate);

			for (int i = 0; i < tasksTotalCount; i++)
			{
				if (tasks[i].Count <= 0)
					Timing.PauseCoroutines(tasksCoroutines[i]);
			}
		}

		[Button("Set State"), TabGroup("Debug")]
		public void SetState(int index) =>
			currentBehaviour.TransitionToState(index);

		[Button("Set State to Previous"), TabGroup("Debug")]
		public void SetStateToPrevious() =>
			currentBehaviour.TransitionToPreviousState();

		[Button("Set Behaviour"), TabGroup("Debug")]
		public void SetBehaviour(int index)
		{
			if (index == currentIndex || index >= behaviours.Length || index < 0)
				return;

			previousBehaviour = currentBehaviour;
			previousBehaviour.OnExit();
			previousIndex = currentIndex;

			currentBehaviour = behaviours[index];
			currentBehaviour.OnEnter();
			currentIndex = index;

#if UNITY_EDITOR
			currentBehaviourName = currentBehaviour.Name;
			previousBehaviourName = previousBehaviour.Name;
#endif
		}

		[Button("Set Behaviour To Previous"), TabGroup("Debug")]
		public void SetBehaviourToPrevious()
		{
			if (previousIndex == -1)
				return;

			SetBehaviour(previousIndex);
		}

		private void OnEnable()
		{
			if (isInitialized)
			{
				EnableTasks();
				currentBehaviour.OnEnter();
			}

			isInitialized = true;
		}

		private void OnDisable()
		{
			DisableTasks();

			currentBehaviour.OnExit();
		}

		private void OnDestroy()
		{
			currentBehaviour.OnExit();

			for (int i = 0; i < tasksTotalCount; i++)
				Timing.KillCoroutines(tasksCoroutines[i]);
		}

		public T GetData<T>(ContextKey contextKey) where T : SharedData
		{
			for (int i = 0; i < sharedDataset.Length; i++)
			{
				SharedData sharedData = sharedDataset[i];

				if (sharedData.GetKey() == contextKey)
					return sharedData as T;
			}
			
			return null;
		}

		private IEnumerator<float> Tick()
		{
			int index = (int)TaskSegment.Default;

			while (true)
			{
				ProcessTask(index);

				yield return Timing.WaitForOneFrame;
			}
		}

		private IEnumerator<float> SlowTick()
		{
			int index = (int)TaskSegment.Slow;

			while (true)
			{
				ProcessTask(index);

				yield return Timing.WaitForOneFrame;
			}
		}

		private IEnumerator<float> FixedTick()
		{
			int index = (int)TaskSegment.Fixed;

			while (true)
			{
				ProcessTask(index);

				yield return Timing.WaitForOneFrame;
			}
		}

		private IEnumerator<float> LateTick()
		{
			int index = (int)TaskSegment.Late;

			while (true)
			{
				ProcessTask(index);

				yield return Timing.WaitForOneFrame;
			}
		}

		private void ProcessTask(int index)
		{
			for (int i = tasksAmount[index] - 1; i >= 0; i--)
				tasks[index][i].ProcessTask();
		}

		private void EnableTasks()
		{
			for (int i = 0; i < tasksTotalCount; i++)
			{
				if (tasks[i].Count > 0)
					Timing.ResumeCoroutines(tasksCoroutines[i]);
			}
		}

		private void DisableTasks()
		{
			for (int i = 0; i < tasksTotalCount; i++)
				Timing.PauseCoroutines(tasksCoroutines[i]);
		}

		public void AddTask(Task task, TaskSegment taskSegment)
		{
			int index = (int)taskSegment;

			tasks[index].Add(task);
			tasksAmount[index]++;
		}

		public void RemoveTask(Task task, TaskSegment taskSegment)
		{
			int index = (int)taskSegment;

			tasks[index].Remove(task);
			tasksAmount[index]--;
		}

		public void ToggleTasks()
		{
			for (int i = 0; i < tasksTotalCount; i++)
			{
				int tasksCount = tasks[i].Count;

				if (tasksCount > 0)
					Timing.ResumeCoroutines(tasksCoroutines[i]);
				else
					Timing.PauseCoroutines(tasksCoroutines[i]);
			}
		}

		[System.Serializable]
		private class Behaviour
		{
#if UNITY_EDITOR
			public string Name => name;

			[SerializeField] private string name = "Behaviour";
#endif

			[SerializeField, FoldoutGroup("Events")] private UnityEvent onEnter = null;
			[SerializeField, FoldoutGroup("Events")] private UnityEvent onExit = null;

			[SerializeField, PageList, FoldoutGroup("Composites")] private Composite[] composites = null;

			[SerializeField, PageList, FoldoutGroup("Actions")] private Action[] actions = null;

			[SerializeField, PageList, FoldoutGroup("States")] private State[] states = null;

#if UNITY_EDITOR
			[SerializeField, ReadOnly, FoldoutGroup("Debug")] private string currentStateName = "State";
			[SerializeField, ReadOnly, FoldoutGroup("Debug")] private string previousStateName = "";
#endif

			[SerializeField, ReadOnly, FoldoutGroup("Debug")] private int currentIndex = 0;
			[SerializeField, ReadOnly, FoldoutGroup("Debug")] private int previousIndex = -1;

			private State currentState = null;
			private State previousState = null;

			private BehaviourProcessor behaviourProcessor = null;

			public void Initialize(BehaviourProcessor behaviourProcessor)
			{
				this.behaviourProcessor = behaviourProcessor;

				for (int i = 0; i < composites.Length; i++)
					composites[i].Initialize(behaviourProcessor);

				for (int i = 0; i < actions.Length; i++)
					actions[i].Initialize(behaviourProcessor);

				for (int i = 0; i < states.Length; i++)
					states[i].Initialize(behaviourProcessor);

#if UNITY_EDITOR
				currentStateName = states[0].StateName;
#endif
			}

			public void OnEnter()
			{
				currentState = states[0];
				currentIndex = 0;
				previousIndex = -1;

#if UNITY_EDITOR
				currentStateName = currentState.StateName;
				previousStateName = "";
#endif

				onEnter?.Invoke();

				for (int i = 0; i < composites.Length; i++)
					composites[i].OnEnter();

				for (int i = 0; i < actions.Length; i++)
					actions[i].OnEnter();

				currentState.OnEnter();
				behaviourProcessor.EnableTasks();
			}

			public void OnExit()
			{
				onExit?.Invoke();

				for (int i = 0; i < composites.Length; i++)
					composites[i].OnExit();

				for (int i = 0; i < actions.Length; i++)
					actions[i].OnExit();

				currentState.OnExit();
				behaviourProcessor.DisableTasks();
			}

			public void TransitionToState(int index)
			{
				if (index == currentIndex || index >= states.Length || index < 0)
					return;

				previousState = currentState;
				previousState.OnExit();
				previousIndex = currentIndex;

				currentState = states[index];
				currentState.OnEnter();
				currentIndex = index;

#if UNITY_EDITOR
				currentStateName = currentState.StateName;
				previousStateName = previousState.StateName;
#endif
			}

			public void TransitionToPreviousState()
			{
				if (previousIndex == -1)
					return;

				TransitionToState(previousIndex);
			}
		}
	}
}

