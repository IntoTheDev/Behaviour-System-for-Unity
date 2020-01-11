using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsMoving : Condition
	{
		[SerializeField] private ActorMovement actorMovement = null;

		private IMovementInput movementInput = null;

		public override void OnEnter()
		{
			movementInput = actorMovement.MovementInput;

			if (movementInput != null)
				base.OnEnter();

			actorMovement.OnInputChange += GetInput;
		}

		public override void OnExit()
		{
			actorMovement.OnInputChange -= GetInput;
			base.OnExit();
		}

		private void GetInput(IMovementInput movementInput)
		{
			if (movementInput != null)
			{
				if (this.movementInput == null)
					RunCoroutine();
				else if (this.movementInput != movementInput)
					RunCoroutine();
			}
			else
			{
				this.movementInput = movementInput;
				StopTask();
			}

			void RunCoroutine()
			{
				this.movementInput = movementInput;
				StopTask();
				RunTask();
			}
		}

		public override void ProcessTask()
		{
			bool isMoving = movementInput.Direction == Vector2.zero ? false : true;
			ProcessCondition(isMoving);
		}
	}
}
