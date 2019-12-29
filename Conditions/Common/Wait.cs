using MEC;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class Wait : Condition
	{
		[SerializeField, FoldoutGroup("Data")] private float time = 1f;

		private CoroutineHandle coroutineHandle = default;

		public override void OnEnter() =>
			coroutineHandle = Timing.RunCoroutine(WaitTime().CancelWith(cachedObject));

		public override void OnExit() =>
			Timing.KillCoroutines(coroutineHandle);

		private IEnumerator<float> WaitTime()
		{
			yield return Timing.WaitForSeconds(time);
			ProcessCondition(true);
		}
	}
}
