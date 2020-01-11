using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Groups;

namespace ToolBox.Behaviours.Actions
{
	public class FollowPlayer : Action
	{
		[SerializeField] private Group playerGroup = null;

		private Transform player = null;
		private AIMovementInput aIMovementInput = null;

		public override void Initialize(BehaviourProcessor behaviour)
		{
			base.Initialize(behaviour);

			player = playerGroup.Members[0].transform;
			aIMovementInput = behaviour.GetComponent<AIMovementInput>();
		}

		public override void OnEnter()
		{
			RunTask();
			//aIMovementInput.OnPathComplete.AddListener(OnPlayerReached);
		}

		public override void OnExit()
		{
			StopTask();
			//aIMovementInput.OnPathComplete.RemoveListener(OnPlayerReached);
		}

		public override void ProcessTask() =>
			aIMovementInput.MoveTo(player.position);

		//private void OnPlayerReached() =>
			//StopTask();
	}
}
