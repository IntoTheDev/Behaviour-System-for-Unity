namespace ToolBox.Behaviours
{
	public class OnEnterProcessor : TaskProcessor
	{
		public override void OnEnter() =>
			task.ProcessTask();

		public override void OnExit() { }
	}
}
