using Sirenix.OdinInspector;
using System;
using ToolBox.Behaviours.Composites;
using ToolBox.Behaviours.Utilities;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class ContextFloat : Context
	{
		[SerializeField, FoldoutGroup("Data")] private CheckType checkType = CheckType.Equals;
		[SerializeField, FoldoutGroup("Data")] private float value = 0;

		private float sharedValue => (float)sharedData.Value;
		private Action operatorAction = null;

		public override void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			base.Initialize(composite, behaviour);

			switch (checkType)
			{
				case CheckType.Equals:
					operatorAction = Equals;
					break;

				case CheckType.Greater:
					operatorAction = Greater;
					break;

				case CheckType.GreaterOrEqual:
					operatorAction = GreaterOrEqual;
					break;

				case CheckType.Less:
					operatorAction = Less;
					break;


				case CheckType.LessOrEqual:
					operatorAction = LessOrEqual;
					break;
			}
		}

		private void Equals()
		{
			bool result = sharedValue == value;
			ProcessCondition(result);
		}

		private void Greater()
		{
			bool result = sharedValue > value;
			ProcessCondition(result);
		}

		private void GreaterOrEqual()
		{
			bool result = sharedValue >= value;
			ProcessCondition(result);
		}

		private void Less()
		{
			bool result = sharedValue < value;
			ProcessCondition(result);
		}

		private void LessOrEqual()
		{
			bool result = sharedValue <= value;
			ProcessCondition(result);
		}

		public override void Process() =>
			operatorAction();
	}
}
