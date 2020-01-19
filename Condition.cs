using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition : Task
	{
		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		private Composite composite = null;
		private bool previousResult = false;
		private bool wasProcessed = false;

		public void SetComposite(Composite composite) =>
			this.composite = composite;

		public override void OnEnter()
		{
			base.OnEnter();
			wasProcessed = false;
		}

		protected void ProcessCondition(bool result)
		{
			result = (result && !isNot) || (!result && isNot);

			if (result != previousResult || !wasProcessed)
				composite.ProcessCondition(result, this);

			previousResult = result;
			wasProcessed = true;
		}
	}
}
