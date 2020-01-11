using UnityEngine;
using ToolBox.Groups;

namespace ToolBox.Behaviours.Conditions
{
	public class IsInGroup : Condition
	{
		[SerializeField] private Group group = null;

		public override void ProcessTask()
		{
			bool result = group.HasMember(cachedObject);
			ProcessCondition(result);
		}
	}
}
