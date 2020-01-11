using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition : Task          
	{
		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		private Composite composite = null;

		public virtual void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			InitializeTask(behaviour);

			this.composite = composite;
			behaviourProcessor = behaviour;
			cachedTransform = behaviour.transform;
			cachedObject = behaviour.gameObject; 
		}

		protected void ProcessCondition(bool result)
		{
			result = (result && !isNot) || (!result && isNot);
			composite.ProcessCondition(result, this);
		}
	}
}
