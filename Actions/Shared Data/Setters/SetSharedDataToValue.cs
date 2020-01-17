using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class SetSharedDataToValue<T, C> : SharedDataSetter<T, C> where C : ContextKey
	{
		[SerializeField] private T value = default;

		public override void OnEnter() =>
			sharedTo.Value = value;

		public override void OnExit() { }
	}
}

