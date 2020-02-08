using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	[TypeInfoBox("Use Custom Processor")]
	public class HasTarget : Condition
	{
		private TargetingBehaviour targetingBehaviour = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			targetingBehaviour = behaviourProcessor.GetComponent<TargetingBehaviour>();
		}

		public override void OnEnter()
		{
			base.OnEnter();

			targetingBehaviour.OnTargetFound += OnTargetFound;
			targetingBehaviour.OnTargetLost += OnTargetLost;
		}

		public override void OnExit()
		{
			base.OnExit();

			targetingBehaviour.OnTargetFound -= OnTargetFound;
			targetingBehaviour.OnTargetLost -= OnTargetLost;
		}

		private void OnTargetFound(Transform target)
		{
			if (target != null)
				ProcessCondition(true);
		}            

		private void OnTargetLost(Transform target) =>
			ProcessCondition(false);

		public override void ProcessTask() { }
	}
}
