using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class CompareWithSharedData<T, C> : SharedDataComparer<T, C> where C : ContextKey
	{
		[SerializeField] private C contextFrom = null;

		private SharedData<T, C> otherData = null;

		public override void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(contextKey, behaviourProcessor);

			otherData = behaviourProcessor.GetData<SharedData<T, C>>(contextFrom);
		}

		public override void OnEnter()
		{
			OnDataChanged();

			sharedDataToCompare.OnValueChanged += OnDataChanged;
			otherData.OnValueChanged += OnDataChanged;
		}

		public override void OnExit()
		{
			sharedDataToCompare.OnValueChanged -= OnDataChanged;
			otherData.OnValueChanged -= OnDataChanged;
		}

		private void OnDataChanged() =>
			Invoke(equalityComparer.Equals(sharedDataToCompare.Value, otherData.Value));
	}
}
