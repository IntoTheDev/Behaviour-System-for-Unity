namespace ToolBox.Behaviours.Actions
{
	public abstract class Action : Node
	{
		public virtual void Initialize(BehaviourProcessor behaviour)
		{
			InitializeNode();

			this.behaviour = behaviour;
			cachedTransform = behaviour.transform;
			cachedObject = behaviour.gameObject;
		}
	}
}
