using UnityEngine;
using ToolBox.Attributes;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : MonoBehaviour
	{
		public delegate void OnStateChange();
		public event OnStateChange onStateChange = delegate { };

		[ReadOnly, BoxGroup("Debug")] public State currentState = null;
		[ReorderableList, SerializeField, BoxGroup("States")] private State[] states = null;
		[SerializeField, BoxGroup("Debug")] private bool actorActive;

		private int statesCount;

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
			if (actorActive)
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
				actorActive = false;
				Debug.LogError(name + " doesn't have states!");
				enabled = false;
				return;
			}

			for (int i = 0; i < statesCount; i++)
				states[i].InitializeState(this, i);
		}
	}
}

