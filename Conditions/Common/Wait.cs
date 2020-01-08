namespace ToolBox.Behaviours.Conditions
{
	public class Wait : Condition
	{
		protected override void Task() =>
			ProcessCondition(true);
	}
}
