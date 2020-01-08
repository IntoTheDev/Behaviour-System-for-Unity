using ToolBox.Behaviours.Conditions;

namespace ToolBox.Behaviours.Composites
{
	public class Sequence : Composite
	{
		public override void ProcessCondition(bool result, Condition condition)
		{
			base.ProcessCondition(result, condition);

			if (!result)
				onFailure?.Invoke();

			if (currentCount >= conditionsCount)
				onSuccess?.Invoke();
		}
	}
}
