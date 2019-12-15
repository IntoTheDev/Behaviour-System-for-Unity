using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.UtilityAI
{
	public class UtilityAI : SerializedMonoBehaviour
	{
		[OdinSerialize, ListDrawerSettings(NumberOfItemsPerPage = 1, Expanded = true, DraggableItems = false)] private Action[] actions = null;

		private float[] scores = null;
		private int actionsCount = 0;

		private void Awake()
		{
			actionsCount = actions.Length;
			scores = new float[actionsCount];
		}

		[Button("Check", Expanded = true)]
		public void ChooseBehaviour()
		{
			for (int scoreIndex = 0; scoreIndex < actionsCount; scoreIndex++)
				scores[scoreIndex] = 0f;

			for (int actionIndex = 0; actionIndex < actionsCount; actionIndex++)
			{
				Action action = actions[actionIndex];
				int scorersCount = action.Scorers.Length;

				for (int scorerIndex = 0; scorerIndex < scorersCount; scorerIndex++)
				{
					Scorer scorer = action.Scorers[scorerIndex];
					bool result = scorer.Execute();
					bool isNot = scorer.IsNot;

					result = (result && !isNot) || (!result && isNot);

					if (result)
						scores[actionIndex] += scorer.Score;
				}
			}

			int bestIndex = 0;
			float bestScore = scores[bestIndex];

			for (int i = 1; i < actionsCount; i++)
			{
				if (scores[i] > bestScore)
				{
					bestIndex = i;
					bestScore = scores[bestIndex];
				}	
			}

			actions[bestIndex].OnSuccess?.Invoke();
		}

		[System.Serializable]
		private struct Action
		{
			public Scorer[] Scorers => scorers;
			public UnityEvent OnSuccess => onSuccess;

			[SerializeField] private string debugName;
			[SerializeField, ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 1, DraggableItems = false), TabGroup("Scorers")] private Scorer[] scorers;

			[SerializeField, TabGroup("Events")] private UnityEvent onSuccess;
		}
	}
}
