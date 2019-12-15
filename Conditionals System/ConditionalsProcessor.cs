using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Conditionals
{
	public class ConditionalsProcessor : SerializedMonoBehaviour
	{
		[OdinSerialize, ListDrawerSettings(NumberOfItemsPerPage = 1, Expanded = true, DraggableItems = false)] private ConditionalsContainer[] conditionalsContainers = null;

		[Button("Check", Expanded = true)]
		public void CheckConditionals(int index)
		{
			ConditionalsContainer conditionalsContainer = conditionalsContainers[index];
			Conditional[] conditionals = conditionalsContainer.Conditionals;
			int count = conditionals.Length;

			for (int i = 0; i < count; i++)
			{
				Conditional conditional = conditionals[i];

				bool condition = conditional.Execute();
				bool isNot = conditional.IsNot;
				bool result = condition && !isNot || !condition && isNot;

				if (!result)
				{
					conditionalsContainer.OnFailure?.Invoke();
					Debug.Log(i);
					return;
				}
			}

			conditionalsContainer.OnSuccess?.Invoke();
		}

		[System.Serializable]
		private struct ConditionalsContainer
		{
			public Conditional[] Conditionals => conditionals;
			public UnityEvent OnSuccess => onSuccess;
			public UnityEvent OnFailure => onFailure;

			[SerializeField] private string debugName;
			[SerializeField, ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 1, DraggableItems = false), TabGroup("Conditions")] private Conditional[] conditionals;

			[SerializeField, TabGroup("Events")] private UnityEvent onSuccess;
			[SerializeField, TabGroup("Events")] private UnityEvent onFailure;
		}
	}
}
