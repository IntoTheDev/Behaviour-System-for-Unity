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
		public int StateIndex { get; private set; }
		public int StatesCount => statesCount;

		[ReorderableList, SerializeField, BoxGroup("Data")] private State[] states = null;
		[SerializeField, ReadOnly, BoxGroup("Debug")] private State currentState = null;
		[SerializeField, BoxGroup("Debug")] private bool entityActive = true;

		private int statesCount = 0;

		private void Start()
		{
			statesCount = states.Length;

			if (statesCount == 0 || states[0] == null)
				return;

			for (int i = 0; i < statesCount; i++)
				states[i].SetIndex(i);

			currentState = states[0];
			currentState.EnterState();
			OnStateChange?.Invoke(currentState);
		}

		private void OnValidate() => statesCount = states.Length;

		public void TransitionToState(int index)
		{
#if UNITY_EDITOR
			if (!entityActive)
				return;
#endif

			State nextState = states[index];

			currentState.ExitState();

			currentState = nextState;
			StateIndex = index;

			currentState.EnterState();

			OnStateChange?.Invoke(currentState);
		}
	}
}

