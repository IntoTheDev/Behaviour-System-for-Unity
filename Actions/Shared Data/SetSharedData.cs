using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	[TypeInfoBox("Use Custom Processor")]
	public abstract class SetSharedData<T, C> : Action where C : ContextKey
	{
		[SerializeField, FoldoutGroup("Data")] private C contextTo = null;
		[SerializeField, FoldoutGroup("Data")] private SharedDataSetter<T, C> sharedDataSetter = null;

		public override void Initialize(BehaviourProcessor behaviour)
		{
			base.Initialize(behaviour);

			sharedDataSetter.Initialize(contextTo, behaviour);
		}

		public override void OnEnter() =>
			sharedDataSetter.OnEnter();

		public override void OnExit() =>
			sharedDataSetter.OnExit();

		public override void ProcessTask() { }
	}
}

