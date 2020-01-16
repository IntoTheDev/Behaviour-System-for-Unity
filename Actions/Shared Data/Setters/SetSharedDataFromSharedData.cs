using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class SetSharedDataFromSharedData<T, C> : SharedDataSetter<T, C> where C : ContextKey
	{
		[SerializeField] private C contextFrom = null;

		private SharedData sharedFrom = null;

		public override void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(contextKey, behaviourProcessor);

			sharedFrom = behaviourProcessor.GetData<SharedData>(contextFrom);
		}

		public override void SetValue() =>
			sharedTo.SetValueFromSharedData(sharedFrom);
	}
}

