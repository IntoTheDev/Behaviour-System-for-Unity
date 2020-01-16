namespace ToolBox.Behaviours.Conditions
{
	public abstract class SharedDataComparer<T, C> where C : ContextKey
	{
		protected BehaviourProcessor behaviourProcessor = null;
		protected SharedData sharedDataToCompare = null;

		public virtual void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			this.behaviourProcessor = behaviourProcessor;
			sharedDataToCompare = behaviourProcessor.GetData<SharedData>(contextKey);
		}

		public abstract bool Compare();
	}
}
