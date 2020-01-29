using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsMoving : Condition
	{
		private IMovementInput movementInput = null;
		private ActorMovement actorMovement = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			actorMovement = behaviourProcessor.GetComponent<ActorMovement>();
			movementInput = actorMovement.MovementInput;
			actorMovement.OnInputChange += SetInput;
		}

		public override void OnEnter()
		{
			base.OnEnter();

			actorMovement.OnInputChange += SetInput;
		}

		public override void ProcessTask()
		{
			bool result = movementInput.Direction.x != 0f;
			ProcessCondition(result);
		}

		public override void OnExit()
		{
			base.OnExit();

			actorMovement.OnInputChange -= SetInput;
		}

		private void SetInput(IMovementInput movementInput) =>
			this.movementInput = movementInput;
	}
}
