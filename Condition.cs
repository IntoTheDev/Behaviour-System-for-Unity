using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition
	{
		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		protected BehaviourProcessor behaviour = null;
		protected Transform cachedTransform = null;
		protected GameObject cachedObject = null;

		private Composite composite = null;

		public virtual void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			this.composite = composite;
			this.behaviour = behaviour;
			cachedTransform = behaviour.transform;
			cachedObject = behaviour.gameObject;
		}

		public abstract void OnEnter();

		public abstract void OnExit();

		public void ProcessCondition(bool result)
		{
			result = (result && !isNot) || (!result && isNot);
			composite.ProcessCondition(result, this);
		}
	}
}
