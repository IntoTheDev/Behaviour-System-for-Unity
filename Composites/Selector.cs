using ToolBox.Behaviours.Conditions;

namespace ToolBox.Behaviours.Composites
{
	public class Selector : Composite
	{
		public override void ProcessCondition(bool result, Condition condition)
		{
			base.ProcessCondition(result, condition);

			if (result)
				OnSuccess?.Invoke();

			if (falseCount >= conditionsCount)
				OnFailure?.Invoke();
		}
	}
}
