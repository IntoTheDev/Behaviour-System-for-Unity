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

		[Button("Conditions", Expanded = true)]
		public void ProcessConditions()
		{
			int bestIndex = 0;
			float bestScore = 0f;
			float totalScore = 0f;

			for (int actionIndex = 0; actionIndex < actionsCount; actionIndex++)
			{
				Action action = actions[actionIndex];
				int scorersCount = action.Scorers.Length;
				scores[actionIndex] = 0f;

				for (int scorerIndex = 0; scorerIndex < scorersCount; scorerIndex++)
				{
					Scorer scorer = action.Scorers[scorerIndex];
					bool result = scorer.Execute();
					bool isNot = scorer.IsNot;

					result = (result && !isNot) || (!result && isNot);

					if (result)
						scores[actionIndex] += scorer.Score;
				}

				float score = scores[actionIndex];
				totalScore += score;

				if (score > bestScore)
				{
					bestIndex = actionIndex;
					bestScore = score;
				}
			}

			if (totalScore != 0f)
				actions[bestIndex].OnSuccess?.Invoke();
		}

		[Button("Condition", Expanded = true)]
		public void ProcessCondition(int index)
		{
			Action action = actions[index];
			Scorer[] scorers = action.Scorers;
			int count = scorers.Length;

			for (int i = 0; i < count; i++)
			{
				Scorer scorer = action.Scorers[i];
				bool result = scorer.Execute();
				bool isNot = scorer.IsNot;

				result = (result && !isNot) || (!result && isNot);

				if (!result)
				{
					action.OnFailure?.Invoke();
					return;
				}
			}

			action.OnSuccess?.Invoke();
		}

		[System.Serializable]
		private struct Action
		{
			public Scorer[] Scorers => scorers;
			public UnityEvent OnSuccess => onSuccess;
			public UnityEvent OnFailure => onFailure;

			[SerializeField] private string debugName;
			[SerializeField, ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 1, DraggableItems = false), TabGroup("Scorers")] private Scorer[] scorers;

			[SerializeField, TabGroup("Events")] private UnityEvent onSuccess;
			[SerializeField, TabGroup("Events")] private UnityEvent onFailure;
		}
	}
}
