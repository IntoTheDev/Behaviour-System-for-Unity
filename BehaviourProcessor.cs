using MEC;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : SerializedMonoBehaviour
	{
		[OdinSerialize, TabGroup("Context"), ListDrawerSettings(
			DraggableItems = false,
			NumberOfItemsPerPage = 1,
			Expanded = true)]
		private Dictionary<ContextKey, SharedData<object>> context = null;

		[OdinSerialize, ListDrawerSettings(
			NumberOfItemsPerPage = 1,
			Expanded = true,
			DraggableItems = false), TabGroup("Data")]
		private State[] states = null;

#if UNITY_EDITOR
		[SerializeField, ReadOnly, TabGroup("Debug")] private string currentStateName = "State";
		[SerializeField, ReadOnly, TabGroup("Debug")] private string previousStateName = "State";
#endif

		[SerializeField, ReadOnly, TabGroup("Debug")] private int currentIndex = 0;
		[SerializeField, ReadOnly, TabGroup("Debug")] private int previousIndex = 0;

		private State currentState = null;
		private State previousState = null;

		private bool isInitialized = false;

		private List<Task>[] tasks = null;
		private int[] tasksAmount = null;
		private CoroutineHandle[] tasksCoroutines = null;

		private const int tasksTotalCount = 4;

		private void Start()
		{
			tasks = new List<Task>[tasksTotalCount];
			tasksCoroutines = new CoroutineHandle[tasksTotalCount];
			tasksAmount = new int[tasksTotalCount];

			int tasksInitialCapacity = 10;

			for (int i = 0; i < tasksTotalCount; i++)
				tasks[i] = new List<Task>(tasksInitialCapacity);

			if (states.Length == 0 || states[0] == null)
			{
				enabled = false;
				return;
			}

			for (int i = 0; i < states.Length; i++)
				states[i].Initialize(this);

			currentState = states[0];
			currentState.OnEnter();

#if UNITY_EDITOR
			currentStateName = currentState.StateName;
#endif

			EnableTasks();
		}

		private void OnEnable()
		{
			if (isInitialized)
			{
				currentState.OnEnter();
				EnableTasks();
			}

			isInitialized = true;
		}

		private void OnDisable()
		{
			currentState.OnExit();
			DisableTasks();
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
			if (previousState == null)
				return;

			TransitionToState(previousIndex);
		}

		public SharedData<object> GetData(ContextKey contextKey)
		{
			context.TryGetValue(contextKey, out SharedData<object> value);
			return value;
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
			tasksCoroutines[(int)TaskSegment.Default] = Timing.RunCoroutine(Tick(), Segment.Update);
			tasksCoroutines[(int)TaskSegment.Slow] = Timing.RunCoroutine(SlowTick(), Segment.SlowUpdate);
			tasksCoroutines[(int)TaskSegment.Fixed] = Timing.RunCoroutine(FixedTick(), Segment.FixedUpdate);
			tasksCoroutines[(int)TaskSegment.Late] = Timing.RunCoroutine(LateTick(), Segment.LateUpdate);
		}

		private void DisableTasks()
		{
			for (int i = 0; i < tasksTotalCount; i++)
				Timing.KillCoroutines(tasksCoroutines[i]);
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
	}
}

