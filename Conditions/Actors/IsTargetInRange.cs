using MEC;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsTargetInRange : TargetingCondition
	{
		[SerializeField, FoldoutGroup("Data")] private float range = 1f;

		private TargetingDistance targetingDistance = null;

		public override void OnEnter()
		{
			base.OnEnter();

			targetingDistance = behaviour.GetComponent<TargetingDistance>();

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

		protected override void Task()
		{
			bool isInRange = targetingDistance.Distance <= range;
			ProcessCondition(isInRange);
		}
	}
}
