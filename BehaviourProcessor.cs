using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : MonoBehaviour
	{
		[HideInInspector] public UnityAction<State> OnStateChange;

		public State CurrentState => currentState;
		public int StateIndex { get; private set; }
		public int StatesCount => statesCount;

		[SerializeField, FoldoutGroup("Data"), ListDrawerSettings(NumberOfItemsPerPage = 1, Expanded = true, DraggableItems = false)] private State[] states = null;

		[SerializeField, FoldoutGroup("Debug"), ReadOnly] private State currentState = null;
		[SerializeField, FoldoutGroup("Debug")] private bool entityActive = true;

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

		private void OnValidate()
		{
			if (states == null)
				return;

			statesCount = states.Length;

			for (int i = 0; i < statesCount; i++)
				states[i].SetIndex(i);
		}

		public void TransitionToState(int index)
		{
#if UNITY_EDITOR
			if (!entityActive)
				return;
#endif
			if (index == StateIndex)
				return;

			State nextState = states[index];

			currentState.ExitState();

			currentState = nextState;
			StateIndex = index;

			currentState.EnterState();

			OnStateChange?.Invoke(currentState);
		}
	}
}

