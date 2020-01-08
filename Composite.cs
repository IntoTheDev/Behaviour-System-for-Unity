using Sirenix.OdinInspector;
using System.Collections.Generic;
using ToolBox.Behaviours.Conditions;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Composites
{
	public abstract class Composite
	{
		[SerializeField, TabGroup("Events")] protected UnityEvent onSuccess = null;
		[SerializeField, TabGroup("Events")] protected UnityEvent onFailure = null;

		[SerializeField, ListDrawerSettings(
				NumberOfItemsPerPage = 1,
				Expanded = true,
				DraggableItems = false), TabGroup("Data")] private Condition[] conditions = null;

		protected int conditionsCount = 0;
		protected int currentCount = 0;

		private List<Condition> trueConditions = null;
		private List<Condition> falseConditions = null;

		protected int falseCount = 0;

		public void Initialize(BehaviourProcessor behaviour)
		{
			conditionsCount = conditions.Length;

			trueConditions = new List<Condition>(conditionsCount);
			falseConditions = new List<Condition>(conditionsCount);

			for (int i = 0; i < conditionsCount; i++)
				conditions[i].Initialize(this, behaviour);
		}

		public virtual void ProcessCondition(bool result, Condition condition)
		{
			if (result && !trueConditions.Contains(condition))
			{
				trueConditions.Add(condition);

				if (falseConditions.Contains(condition))
				{
					falseConditions.Remove(condition);
					falseCount--;
				}
			}
			else if (!falseConditions.Contains(condition))
			{
				falseConditions.Add(condition);

				if (trueConditions.Contains(condition))
				{
					trueConditions.Remove(condition);
					currentCount--;
				}

				falseCount++;
			}
		}

		public void OnEnter()
		{
			trueConditions.Clear();
			falseConditions.Clear();

			currentCount = 0;
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

