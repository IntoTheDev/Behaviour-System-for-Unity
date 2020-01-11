namespace ToolBox.Behaviours
{
	public abstract class TaskProcessor
	{
		protected Task task = null;

		public virtual void Initialize(Task task) =>
			this.task = task;

		public abstract void OnEnter();

		public abstract void OnExit();
	}
}
