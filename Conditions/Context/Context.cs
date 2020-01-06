using Sirenix.OdinInspector;
using ToolBox.Behaviours.Composites;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Context : Condition
	{
		[SerializeField, FoldoutGroup("Data")] protected ContextKey contextKey = null;

		protected SharedData<object> sharedData = null;

		public override void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			base.Initialize(composite, behaviour);

			sharedData = behaviour.GetData(contextKey);
		}

		public override void OnEnter() =>
			RunTask();

		public override void OnExit() =>
			StopTask();
	}
}
