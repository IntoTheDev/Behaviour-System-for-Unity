using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	[TypeInfoBox("Use Custom Processor")]
	public abstract class CompareSharedData<T, C> : Condition where C : ContextKey
	{
		[SerializeField, FoldoutGroup("Data")] private C contextToCompare = null;
		[SerializeField, FoldoutGroup("Data")] private SharedDataComparer<T, C> sharedDataComparer = null;

		public override void Initialize(BehaviourProcessor behaviour)
		{
			base.Initialize(behaviour);

			sharedDataComparer.Initialize(contextToCompare, behaviour);
		}

		public override void OnEnter()
		{
			sharedDataComparer.OnValueChanged += OnValueChanged;
			sharedDataComparer.OnEnter();
		}

		public override void OnExit()
		{
			sharedDataComparer.OnValueChanged -= OnValueChanged;
			sharedDataComparer.OnExit();
		}

		private void OnValueChanged(bool isEquals) =>
			ProcessCondition(isEquals);

		public override void ProcessTask() { }
	}
}
