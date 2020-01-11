namespace ToolBox.Behaviours.Actions
{
	public class FindTarget : Action
	{
		private TargetingBehaviour targetingBehaviour = null;

		public override void Initialize(BehaviourProcessor behaviour)
		{
			base.Initialize(behaviour);

			targetingBehaviour = behaviour.GetComponent<TargetingBehaviour>();
		}

		public override void OnEnter() =>
			RunTask();

		public override void OnExit() =>
			StopTask();

		public override void ProcessTask() =>
			targetingBehaviour?.FindTarget();
	}
}