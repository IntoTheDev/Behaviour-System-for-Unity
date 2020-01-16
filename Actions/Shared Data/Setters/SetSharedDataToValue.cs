using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class SetSharedDataToValue<T, C> : SharedDataSetter<T, C> where C : ContextKey
	{
		[SerializeField] private T value = default;

		public override void SetValue() =>
			sharedTo.SetValue(value);
	}
}

