using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class CompareWithValue<T, C> : SharedDataComparer<T, C> where C : ContextKey
	{
		[SerializeField] private T value = default;

		public override bool Compare() =>
			sharedDataToCompare.GetValue().Equals(value);
	}
}
