using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class ContextBool : Context
	{
		public override void Process() =>
			ProcessCondition((bool)sharedData.Value);
	}
}
