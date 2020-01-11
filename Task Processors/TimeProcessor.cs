using MEC;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	public class TimeProcessor : TaskProcessor
	{
		[SerializeField] private float updateRate = 1f;

		private CoroutineHandle taskCoroutine = default;

		public override void OnEnter() =>
			taskCoroutine = Timing.RunCoroutine(SecondsUpdate(), Segment.Update);

		public override void OnExit() =>
			Timing.KillCoroutines(taskCoroutine);

		private IEnumerator<float> SecondsUpdate()
		{
			while (true)
			{
				task.ProcessTask();

				yield return Timing.WaitForSeconds(updateRate);
			}
		}
	}
}
