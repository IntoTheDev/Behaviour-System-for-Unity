using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class ChaseTarget : Action
	{
		[SerializeField, FoldoutGroup("Data")] private ContextTransformKey targetKey = null;

		private AIMovementInput movementInput = null;
		private SharedTransform target = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			movementInput = behaviourProcessor.GetComponent<AIMovementInput>();
			target = behaviourProcessor.GetData<SharedTransform>(targetKey);
		}

		public override void ProcessTask() =>
			movementInput.MoveTo(target.Value.position);
	}
}
