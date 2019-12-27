using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public abstract class Action
	{
		protected BehaviourProcessor behaviour = null;

		public virtual void Initialize(BehaviourProcessor behaviour) =>
			this.behaviour = behaviour;

		public abstract void OnEnter();

		public abstract void OnExit();
	}
}
