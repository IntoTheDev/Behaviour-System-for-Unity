using ToolBox.Attributes;
using UnityEngine;

namespace ToolBox.Behaviours.Utilities
{
	[DisallowMultipleComponent, RequireComponent(typeof(BehaviourProcessor))]
	public class BehaviourRandomTransition : MonoBehaviour
	{
		[SerializeField] private BehaviourProcessor behaviourProcessor = null;
		[SerializeField, ReorderableList] private RandomTransition[] randomTransitions = default;

		private void Start()
		{
			for (int i = 0; i < randomTransitions.Length; i++)
				for (int j = 0; j < randomTransitions[i].PossibleStates.Length; j++)
					randomTransitions[i].PossibleStates[j] = Mathf.Clamp(randomTransitions[i].PossibleStates[j], 0, behaviourProcessor.StatesCount - 1);
		}

		public void TransitionToRandomState(int randomTransition)
		{
			RandomTransition newTransition = randomTransitions[randomTransition];
			int randomIndex = newTransition.PossibleStates[Random.Range(0, newTransition.PossibleStates.Length)];

			behaviourProcessor.TransitionToState(randomIndex);
		}

		[System.Serializable]
		private struct RandomTransition
		{
			public int[] PossibleStates => possibleStates;

#if UNITY_EDITOR
			[SerializeField] private string packName;
#endif

			[SerializeField] private int[] possibleStates;
		}
	}
}

