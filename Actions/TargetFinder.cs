using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class TargetFinder : Action
	{
		private TargetingBehaviour targetingBehaviour = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			targetingBehaviour = behaviourProcessor.GetComponent<TargetingBehaviour>();
		}

		public override void ProcessTask() =>
			targetingBehaviour.FindTarget();
	}
}
