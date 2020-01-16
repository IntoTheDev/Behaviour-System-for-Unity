using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition : Task      
	{
		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		private Composite composite = null;

		public void ProcessCondition()
		{
			throw new System.NotImplementedException();
		}

		public void SetComposite(Composite composite) =>
			this.composite = composite;

		protected void ProcessCondition(bool result)
		{
			result = (result && !isNot) || (!result && isNot);
			composite.ProcessCondition(result, this);
		}
	}
}
