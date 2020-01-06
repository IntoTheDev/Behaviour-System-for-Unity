using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	[DisallowMultipleComponent]
	public class BehaviourProcessor : SerializedMonoBehaviour
	{
		[OdinSerialize, FoldoutGroup("Context"), ListDrawerSettings(
			DraggableItems = false,
			NumberOfItemsPerPage = 1,
			Expanded = true)] private Dictionary<ContextKey, SharedData<object>> context = null;

		[OdinSerialize, ListDrawerSettings(
			NumberOfItemsPerPage = 1,
			Expanded = true,
			DraggableItems = false), FoldoutGroup("Data")] private State[] states = null;

#if UNITY_EDITOR
		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private string currentStateName = "State";
		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private string previousStateName = "State";
#endif

		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private int currentIndex = 0;
		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private int previousIndex = 0;

		private State currentState = null;
		private State previousState = null;

		private bool isInitialized = false;

		private void Start()
		{
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
		}

		private void OnEnable()
		{
			if (isInitialized)
				currentState.OnEnter();

			isInitialized = true;
		}

		private void OnDisable() =>
			currentState.OnExit();

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

		public void TransitionToPreviousState() =>
			TransitionToState(previousIndex);

		public SharedData<object> GetData(ContextKey contextKey)
		{
			context.TryGetValue(contextKey, out SharedData<object> value);
			return value;
		}
	}
}

