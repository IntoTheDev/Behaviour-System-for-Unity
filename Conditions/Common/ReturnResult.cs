namespace ToolBox.Behaviours.Conditions
{
	public class ReturnResult : Condition
	{
		public override void ProcessTask() =>
			ProcessCondition(true);
	}
}
