using Sirenix.OdinInspector;
using ToolBox.Behaviours.Actions;
using ToolBox.Behaviours.Composites;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[System.Serializable]
	public class Behaviour
	{
#if UNITY_EDITOR
		public string Name => name;

		[SerializeField] private string name = "Behaviour";

		[SerializeField, AssetSelector] private BehaviourPreset behaviourPreset = null;
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

#if UNITY_EDITOR
		[Button("Save Preset")]
		private void SaveBehaviourPreset()
		{
			if (behaviourPreset == null)
				return;

			behaviourPreset.Behaviour = this;
		}
#endif
	}
}
