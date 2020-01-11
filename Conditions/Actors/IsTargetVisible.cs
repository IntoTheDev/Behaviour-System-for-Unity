using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsTargetVisible : TargetingCondition
	{
		private TargetingLineOfSight targetingLineOfSight = null;

		public override void OnEnter()
		{
			base.OnEnter();

			targetingLineOfSight = behaviourProcessor.GetComponent<TargetingLineOfSight>();

			if (targetingBehaviour.Target != null)
				RunTask();
		}

		public override void OnExit()
		{
			base.OnExit();

			if (targetingBehaviour.Target != null)
				StopTask();
		}

		protected override void OnTargetFound(Transform target) =>
			RunTask();

		protected override void OnTargetLost(Transform target) =>
			StopTask();

		public override void ProcessTask() =>
			ProcessCondition(targetingLineOfSight.IsInLineOfSight);
	}
}
