namespace ToolBox.Behaviours.Actions
{
	public abstract class SharedDataSetter<T, C> where C : ContextKey
	{
		protected BehaviourProcessor behaviourProcessor = null;
		protected SharedData sharedTo = null;

		public virtual void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			this.behaviourProcessor = behaviourProcessor;
			sharedTo = behaviourProcessor.GetData<SharedData>(contextKey);
		}

		public abstract void SetValue();
	}
}

