namespace ToolBox.Behaviours
{
	public class OnExitProcessor : TaskProcessor
	{
		public override void OnEnter() { }

		public override void OnExit() =>
			task.ProcessTask();
	}
}
