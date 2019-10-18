using ToolBox.Attributes;
using UnityEngine;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : MonoBehaviour
	{
		public delegate void OnStateChange();
		public event OnStateChange onStateChange = delegate { };

		public State CurrentState => currentState;

		[ReorderableList, SerializeField, BoxGroup("States")] private State[] states = null;
		[SerializeField, ReadOnly, BoxGroup("Debug")] private State currentState = null;
		[SerializeField, BoxGroup("Debug")] private bool entityActive = true;

		private int statesCount = 0;

		private void Start()
		{
			statesCount = states.Length;
			InitializeStates();

			currentState = states[0];
			currentState.EnterState();
			onStateChange();
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
			if (nextState.StateIndex == currentState.StateIndex)
				return;

			if (currentState != null)
				currentState.ExitState();

			currentState = nextState;

			currentState.EnterState();

			onStateChange();
		}

		public State FindState(string newStateName)
		{
			for (int i = 0; i < statesCount; i++)
				if (newStateName == states[i].StateName)
					return states[i];

			return null;
		}

		private void InitializeStates()
		{
			if (states[0] == null)
			{
				entityActive = false;
				Debug.LogError(name + " doesn't have states!");
				enabled = false;
				return;
			}

			for (int i = 0; i < statesCount; i++)
				states[i].InitializeState(this, i);
		}
	}
}

