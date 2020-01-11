using Sirenix.OdinInspector;

namespace ToolBox.Behaviours.Conditions
{
	[TypeInfoBox("Use Delayed Processor")]
	public class Wait : Condition
	{
		public override void ProcessTask() =>
			ProcessCondition(true);
	}
}
