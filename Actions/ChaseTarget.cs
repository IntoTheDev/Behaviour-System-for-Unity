using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class ChaseTarget : Action, IMovementInput
	{
		[SerializeField, FoldoutGroup("Data")] private float stopDistance = 1f;

		public Vector2 Direction { get; private set; } = default;

		private ActorMovement actorMovement = null;
		private TargetingBehaviour targetingBehaviour = null;
		private TargetingDistance targetingDistance = null;

		private Vector2 currentDirection = default;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			actorMovement = behaviourProcessor.GetComponent<ActorMovement>();
			targetingBehaviour = behaviourProcessor.GetComponent<TargetingBehaviour>();
			targetingDistance = behaviourProcessor.GetComponent<TargetingDistance>();
		}

		public override void OnEnter()
		{
			base.OnEnter();

			actorMovement.SetInput(this);
		}

		public override void OnExit()
		{
			base.OnExit();

			actorMovement.SetInput(null);
		}

		public override void ProcessTask()
		{
			if (targetingDistance.Distance <= stopDistance) 
			{
				currentDirection.x = 0f;
				Direction = currentDirection;

				return;
			}

			float targetPosition = targetingBehaviour.Target.position.x;
			float position = cachedTransform.position.x;

			currentDirection.x = targetPosition >= position ? 1f : -1f;
			Direction = currentDirection;
		}
	}
}
