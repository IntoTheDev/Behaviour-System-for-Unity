using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	public class DelayedProcessor : TaskProcessor
	{
		[SerializeField] private float delay = 1f;

		private CoroutineHandle taskCoroutine = default;

		public override void OnEnter() =>
			taskCoroutine = Timing.RunCoroutine(DelayedUpdate(), Segment.Update);

		public override void OnExit() =>
			Timing.KillCoroutines(taskCoroutine);

		private IEnumerator<float> DelayedUpdate()
		{
			yield return Timing.WaitForSeconds(delay);

			task.ProcessTask();
		}
	}
}
