namespace ToolBox.Behaviours.Conditions
{
	public class Wait : Condition
	{
		public override void OnEnter() =>
			RunTask();

		public override void OnExit() =>
			StopTask();

		protected override void Task() =>
			ProcessCondition(true);
	}
}
