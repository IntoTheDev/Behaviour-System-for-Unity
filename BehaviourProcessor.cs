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
#endif

		[SerializeField, ReadOnly, FoldoutGroup("Debug")] private int currentIndex = 0;

		private State currentState = null;

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

		public void TransitionToState(int index)
		{
			if (index == currentIndex || index >= states.Length || index < 0)
				return;

			currentState.OnExit();

			currentState = states[index];
			currentState.OnEnter();
			currentIndex = index;

#if UNITY_EDITOR
			currentStateName = currentState.StateName;
#endif
		}

		public SharedData<object> GetData(ContextKey contextKey)
		{
			context.TryGetValue(contextKey, out SharedData<object> value);
			return value;
		}
	}
}

