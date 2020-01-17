using Sirenix.OdinInspector;
using UnityEngine;
using ToolBox.Behaviours.Conditions;

namespace ToolBox.Behaviours.Actions
{
	public abstract class TestingShared<T, C> : Condition
		where C : ContextKey
	{
		[SerializeField] private C contextKey = null;
		[SerializeField] private T value = default;

		private SharedData<T, C> sharedData = null;
		//private SharedTransform sharedTransform = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			sharedData = behaviourProcessor.GetData<SharedData<T, C>>(contextKey);
		}

		public override void ProcessTask()
		{
			bool result = sharedData.Value.Equals(value);

			ProcessCondition(result);
		}
	}
}
