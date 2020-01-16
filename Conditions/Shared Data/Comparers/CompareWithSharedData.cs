using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class CompareWithSharedData<T, C> : SharedDataComparer<T, C> where C : ContextKey
	{
		[SerializeField] private C contextFrom = null;

		private SharedData otherData = null;

		public override void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(contextKey, behaviourProcessor);

			otherData = behaviourProcessor.GetData<SharedData>(contextFrom);
		}

		public override bool Compare() =>
			sharedDataToCompare.IsValueEquals(otherData);
	}
}
