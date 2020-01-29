using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class CompareWithValue<T, C> : SharedDataComparer<T, C> where C : ContextKey
	{
		[SerializeField] private T value = default;

		public override void OnEnter() =>
			Invoke(equalityComparer.Equals(sharedDataToCompare.Value, value));

		public override void OnExit() { }
	}
}
