using Sirenix.OdinInspector;
using System.Collections.Generic;
using ToolBox.Behaviours.Conditions;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Composites
{
	public abstract class Composite
	{
		[SerializeField, FoldoutGroup("Events")] protected UnityEvent onSuccess = null;
		[SerializeField, FoldoutGroup("Events")] protected UnityEvent onFailure = null;

		[SerializeField, ListDrawerSettings(
				NumberOfItemsPerPage = 1,
				Expanded = true,
				DraggableItems = false), FoldoutGroup("Conditions")] private Condition[] conditions = null;

		protected int conditionsCount = 0;
		protected int trueCount = 0;
		protected int falseCount = 0;

		private List<Condition> trueConditions = null;
		private List<Condition> falseConditions = null;

		public void Initialize(BehaviourProcessor behaviour)
		{
			conditionsCount = conditions.Length;


			trueConditions = new List<Condition>(conditionsCount);
			falseConditions = new List<Condition>(conditionsCount);

			for (int i = 0; i < conditionsCount; i++)
			{
				Condition condition = conditions[i];

				condition.Initialize(behaviour);
				condition.SetComposite(this);
			}
		}

		public virtual void ProcessCondition(bool result, Condition condition)
		{
			if (result)
			{
				if (!trueConditions.Contains(condition))
				{
					trueConditions.Add(condition);
					trueCount++;

					if (falseConditions.Contains(condition))
					{
						falseConditions.Remove(condition);
						falseCount--;
					}
				}
			}
			else if (!falseConditions.Contains(condition))
			{			
				falseConditions.Add(condition);
				falseCount++;

				if (trueConditions.Contains(condition))
				{
					trueConditions.Remove(condition);
					trueCount--;
				}
			}
		}

		public void OnEnter()
		{
			trueConditions.Clear();
			falseConditions.Clear();

			trueCount = 0;
			falseCount = 0;

			for (int i = 0; i < conditionsCount; i++)
				conditions[i].OnEnter();
		}

		public void OnExit()
		{
			for (int i = 0; i < conditionsCount; i++)
				conditions[i].OnExit();
		}
	}
}

