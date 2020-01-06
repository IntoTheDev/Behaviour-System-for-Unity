﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsHasTarget : TargetingCondition
	{
		[InfoBox("Use UptadeType.Once", InfoMessageType.Warning)]
		[SerializeField, FoldoutGroup("Debug"), ReadOnly] private bool isHasTarget = false;

		public override void OnEnter()
		{
			base.OnEnter();

			isHasTarget = targetingBehaviour.Target != null;
			RunTask();
		}

		public override void OnExit()
		{
			base.OnExit();

			StopTask();
		}

		protected override void OnTargetFound(Transform target)
		{
			isHasTarget = target != null;
			Task();
		}

		protected override void OnTargetLost(Transform target)
		{
			isHasTarget = false;
			Task();
		}

		protected override void Task() =>
			ProcessCondition(isHasTarget);
	}
}