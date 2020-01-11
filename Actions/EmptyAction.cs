using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class EmptyAction : Action
	{
		public override void ProcessTask() =>
			Debug.Log("Processing...");
	}
}
