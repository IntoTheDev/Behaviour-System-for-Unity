using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class RandomWalking : Action, IMovementInput
	{
		public Vector2 Direction { get; private set; }

		[SerializeField, HideIf("randomDirection"), FoldoutGroup("Data")] private float startDirection = 1f;
		[SerializeField, FoldoutGroup("Data")] private bool randomDirection = true;

		[SerializeField, FoldoutGroup("Raycast")] private float range = 1f;
		[SerializeField, FoldoutGroup("Raycast")] private LayerMask layerMask = default;

		private ActorMovement actorMovement = null;
		private Vector2 currentDirection = default;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			actorMovement = behaviourProcessor.GetComponent<ActorMovement>();
			currentDirection.x = randomDirection ? GetRandomDirection() : startDirection;
		}

		public override void OnEnter()
		{
			base.OnEnter();

			actorMovement.SetInput(this);
			currentDirection.x = GetRandomDirection();
		}

		public override void OnExit()
		{
			base.OnExit();

			actorMovement.SetInput(null);
		}

		public override void ProcessTask()
		{
			RaycastHit2D hit = Physics2D.Raycast(cachedTransform.position, currentDirection, range, layerMask);

			if (hit)
				currentDirection = -currentDirection;

			Direction = currentDirection;
		}

		private float GetRandomDirection() =>
			Extensions.Extensions.Choose(-1f, 1f);
	}
}
