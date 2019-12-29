using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public abstract class Action
	{
		protected BehaviourProcessor behaviour = null;
		protected Transform cachedTrasform = null;
		protected GameObject cachedObject = null;

		public virtual void Initialize(BehaviourProcessor behaviour)
		{
			this.behaviour = behaviour;
			cachedTrasform = behaviour.transform;
			cachedObject = behaviour.gameObject;
		}

		public abstract void OnEnter();

		public abstract void OnExit();
	}
}
