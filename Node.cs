using MEC;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	public abstract class Node
	{
		[SerializeField, FoldoutGroup("Setup")] private UpdateType updateType = UpdateType.Frame;
		[SerializeField, FoldoutGroup("Setup"), ShowIf("updateType", UpdateType.Frame)] private Segment segment = Segment.SlowUpdate;
		[SerializeField, FoldoutGroup("Setup"), ShowIf("updateType", UpdateType.Seconds)] private float updateRate = 1f;
		[SerializeField, FoldoutGroup("Setup"), ShowIf("updateType", UpdateType.Delayed)] private float delay = 1f;

		protected BehaviourProcessor behaviour = null;
		protected Transform cachedTransform = null;
		protected GameObject cachedObject = null;

		private IEnumerator<float> task = null;
		private CoroutineHandle taskCoroutine = default;

		protected void InitializeNode()
		{
			switch (updateType)
			{
				case UpdateType.Frame:
					task = FrameUpdate();
					break;

				case UpdateType.Seconds:
					task = SecondsUpdate();
					segment = Segment.Update;
					break;

				case UpdateType.Delayed:
					task = DelayedUpdate();
					segment = Segment.Update;
					break;
			}
		}

		protected abstract void Task();

		public abstract void OnEnter();

		public abstract void OnExit();

		public void RunTask()
		{
			if (updateType != UpdateType.Once)
				taskCoroutine = Timing.RunCoroutine(task, segment);
			else
				Task();
		}

		public void StopTask()
		{
			if (updateType != UpdateType.Once)
				Timing.KillCoroutines(taskCoroutine);
		}

		private IEnumerator<float> FrameUpdate()
		{
			while (true)
			{
				Task();

				yield return Timing.WaitForOneFrame;
			}
		}

		private IEnumerator<float> SecondsUpdate()
		{
			while (true)
			{
				Task();

				yield return Timing.WaitForSeconds(updateRate);
			}
		}

		private IEnumerator<float> DelayedUpdate()
		{
			yield return Timing.WaitForSeconds(delay);
			Task();
		}

		private enum UpdateType
		{
			Once,
			Frame,
			Seconds,
			Delayed
		}
	}
}
