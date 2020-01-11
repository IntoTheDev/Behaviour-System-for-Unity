namespace ToolBox.Behaviours.Actions
{
	public abstract class Action : Task
	{
		public virtual void Initialize(BehaviourProcessor behaviour)
		{
			InitializeTask(behaviour);

			behaviourProcessor = behaviour;
			cachedTransform = behaviour.transform;
			cachedObject = behaviour.gameObject;
		}
	}
}
