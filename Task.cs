using MEC;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.Behaviours
{
	public abstract class Task
	{
		public BehaviourProcessor BehaviourProcessor => behaviourProcessor;

		[SerializeField, FoldoutGroup("Setup")] private TaskProcessor taskProcessor = null;

		protected BehaviourProcessor behaviourProcessor = null;
		protected Transform cachedTransform = null;
		protected GameObject cachedObject = null;

		protected void InitializeTask(BehaviourProcessor behaviourProcessor)
		{
			this.behaviourProcessor = behaviourProcessor;
			taskProcessor.Initialize(this);
		}

		public virtual void OnEnter() =>
			taskProcessor.OnEnter();

		public virtual void OnExit() =>
			taskProcessor.OnExit();

		protected void RunTask() =>
			taskProcessor.OnEnter();

		protected void StopTask() =>
			taskProcessor.OnExit();

		public abstract void ProcessTask();
	}
}
