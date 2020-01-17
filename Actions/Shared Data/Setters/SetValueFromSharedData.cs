using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class SetValueFromSharedData<T, C> : SharedDataSetter<T, C> where C : ContextKey
	{
		[SerializeField] private C contextFrom = null;

		private SharedData<T, C> sharedFrom = null;

		public override void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(contextKey, behaviourProcessor);

			sharedFrom = behaviourProcessor.GetData<SharedData<T, C>>(contextFrom);
		}

		public override void OnEnter()
		{
			sharedTo.Value = sharedFrom.Value;
			sharedFrom.OnValueChanged += OnValueChanged;
		}

		public override void OnExit() =>
			sharedFrom.OnValueChanged -= OnValueChanged;

		private void OnValueChanged() =>
			sharedTo.Value = sharedFrom.Value;
	}
}

