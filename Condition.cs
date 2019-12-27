using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Condition
	{
		public bool IsNot => isNot;

		[SerializeField, FoldoutGroup("Setup")] private bool isNot = false;

		protected BehaviourProcessor behaviour = null;
		private Composite composite = null;

		public virtual void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			this.composite = composite;
			this.behaviour = behaviour;
		}

		public abstract void OnEnter();

		public abstract void OnExit();

		public void ProcessCondition(bool result)
		{
			result = (result && !IsNot) || (!result && IsNot);
			composite.ProcessCondition(result, this);
		}
	}
}
