using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class ContextBool : Context
	{
		public override void ProcessTask() =>
			ProcessCondition((bool)sharedData.Value);
	}
}
