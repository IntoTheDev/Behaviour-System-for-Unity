using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class ContextBool : Context
	{
		protected override void Task() =>
			ProcessCondition((bool)sharedData.Value);
	}
}
