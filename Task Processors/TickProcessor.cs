using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours
{
	public class TickProcessor : TaskProcessor
	{
		[SerializeField] private TaskSegment taskSegment = TaskSegment.Default;
		[SerializeField, ReadOnly] private bool isProcessing = false;

		private BehaviourProcessor behaviourProcessor = null;

		public override void Initialize(Task task)
		{
			base.Initialize(task);

			behaviourProcessor = task.BehaviourProcessor;
		}

		public override void OnEnter()
		{
			if (isProcessing)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Attempt to start an already running task");
#endif
				return;
			}

			behaviourProcessor.AddTask(task, taskSegment);
			isProcessing = true;
		}

		public override void OnExit()
		{
			if (!isProcessing)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Attempt to complete an already completed task");
#endif
				return;
			}

			behaviourProcessor.RemoveTask(task, taskSegment);
			isProcessing = false;
		}
	}
}
