using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition : Node          
	{
		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		private Composite composite = null;

		public virtual void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			InitializeNode();

			this.composite = composite;
			this.behaviour = behaviour;
			cachedTransform = behaviour.transform;
			cachedObject = behaviour.gameObject; 
		}

		public void ProcessCondition(bool result)
		{
			result = (result && !isNot) || (!result && isNot);
			composite.ProcessCondition(result, this);
		}
	}
}
