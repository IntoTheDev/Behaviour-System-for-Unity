using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class TargetingCondition : Condition
	{
		protected TargetingBehaviour targetingBehaviour = null;

		public override void OnEnter()
		{
			targetingBehaviour = behaviourProcessor.GetComponent<TargetingBehaviour>();

			targetingBehaviour.OnTargetFound.AddListener(OnTargetFound);
			targetingBehaviour.OnTargetLost.AddListener(OnTargetLost);
		}

		public override void OnExit()
		{
			targetingBehaviour.OnTargetFound.RemoveListener(OnTargetFound);
			targetingBehaviour.OnTargetLost.RemoveListener(OnTargetLost);
		}

		protected abstract void OnTargetFound(Transform target);

		protected abstract void OnTargetLost(Transform target);
	}
}
