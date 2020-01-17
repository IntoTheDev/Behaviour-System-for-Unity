using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Actions
{
	public class PatrolSpawn : Action
	{
		[SerializeField, FoldoutGroup("Data")] private UnityEvent onPositionReached = null;
		[SerializeField, FoldoutGroup("Data")] private float patrolRadius = 3f;

		private AIMovementInput movementInput = null;
		private Vector3 startPosition = default;
		private Vector3 randomPosition = default;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			movementInput = behaviourProcessor.GetComponent<AIMovementInput>();
			startPosition = cachedTransform.position;
		}

		public override void OnEnter()
		{
			base.OnEnter();

			movementInput.OnPositionReached += OnPositionReached;

			randomPosition = new Vector3
			{
				x = startPosition.x + Random.Range(-patrolRadius, patrolRadius),
				y = startPosition.y + Random.Range(-patrolRadius, patrolRadius)
			};
		}

		public override void OnExit()
		{
			base.OnExit();

			movementInput.OnPositionReached -= OnPositionReached;
		}

		public override void ProcessTask() =>
			movementInput.MoveTo(randomPosition);

		private void OnPositionReached() =>
			onPositionReached?.Invoke();
	}
}
