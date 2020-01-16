using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class CompareSharedData<T, C> : Condition where C : ContextKey
	{
		[SerializeField, FoldoutGroup("Data")] private C contextToCompare = null;
		[SerializeField, FoldoutGroup("Data")] private SharedDataComparer<T, C> sharedDataComparer = null;

		public override void Initialize(BehaviourProcessor behaviour)
		{
			base.Initialize(behaviour);

			sharedDataComparer.Initialize(contextToCompare, behaviour);
		}

		public override void ProcessTask() =>
			ProcessCondition(sharedDataComparer.Compare());
	}
}
