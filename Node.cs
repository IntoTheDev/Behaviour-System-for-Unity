using MEC;
using Sirenix.OdinInspector;
using System;
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
		private Action internalRun = null;
		private Action internalStop = null;

		protected void InitializeNode()
		{
			switch (updateType)
			{
				case UpdateType.Once:
					internalRun = InternalRunTaskOnce;
					internalStop = InternalStopEmptyTask;
					break;

				case UpdateType.Frame:
					task = FrameUpdate();
					internalRun = InternalRunTask;
					internalStop = InternalStopTask;
					break;

				case UpdateType.Seconds:
					task = SecondsUpdate();
					segment = Segment.Update;
					internalRun = InternalRunTask;
					internalStop = InternalStopTask;
					break;

				case UpdateType.Delayed:
					task = DelayedUpdate();
					segment = Segment.Update;
					internalRun = InternalRunTask;
					internalStop = InternalStopTask;
					break;
			}
		}

		public virtual void OnEnter() =>
			RunTask();

		public virtual void OnExit() =>
			StopTask();

		protected void RunTask() =>
			internalRun();

		protected void StopTask() =>
			internalStop();

		private void InternalRunTask() =>
			taskCoroutine = Timing.RunCoroutine(task, segment);

		private void InternalRunTaskOnce() =>
			Task();

		private void InternalStopTask() =>
			Timing.KillCoroutines(taskCoroutine);

		private void InternalStopEmptyTask() { }

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

		protected abstract void Task();

		private enum UpdateType
		{
			Once,
			Frame,
			Seconds,
			Delayed
		}
	}
}
