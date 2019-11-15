using ToolBox.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : MonoBehaviour
	{
		public UnityAction<State> OnStateChange;

		public State CurrentState => currentState;

		[ReorderableList, SerializeField, BoxGroup("States")] private State[] states = null;
		[SerializeField, ReadOnly, BoxGroup("Debug")] private State currentState = null;
		[SerializeField, BoxGroup("Debug")] private bool entityActive = true;

		private int statesCount = 0;

		private void Start()
		{
			statesCount = states.Length;

			if (statesCount == 0 || states[0] == null)
				return;

			InitializeStates();

			currentState = states[0];
			currentState.EnterState();
			OnStateChange?.Invoke(currentState);
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (entityActive)
#endif
				currentState.UpdateState();
		}

		public void TransitionToState(State nextState)
		{
			InternalTransition(nextState);
		}

		public void TransitionToState(string name)
		{
			State nextState = FindState(name);
			InternalTransition(nextState);
		}

		public State FindState(string newStateName)
		{
			for (int i = 0; i < statesCount; i++)
				if (newStateName == states[i].StateName)
					return states[i];

			return null;
		}

		private void InternalTransition(State nextState)
		{
			if (nextState.StateIndex == currentState.StateIndex)
				return;

			if (currentState != null)
				currentState.ExitState();

			currentState = nextState;

			currentState.EnterState();

			OnStateChange?.Invoke(currentState);
		}

		private void InitializeStates()
		{
			for (int i = 0; i < statesCount; i++)
				states[i].InitializeState(this, i);
		}
	}
}

