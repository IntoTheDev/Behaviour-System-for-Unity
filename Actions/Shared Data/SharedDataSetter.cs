namespace ToolBox.Behaviours.Actions
{
	public abstract class SharedDataSetter<T, C> where C : ContextKey
	{
		protected SharedData<T, C> sharedTo = null;

		public virtual void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor) =>
			sharedTo = behaviourProcessor.GetData<SharedData<T, C>>(contextKey);

		public abstract void OnEnter();

		public abstract void OnExit();
	}
}

