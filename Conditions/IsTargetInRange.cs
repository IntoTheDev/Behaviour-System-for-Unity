using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class IsTargetInRange : Condition
	{
		[SerializeField, FoldoutGroup("Data")] private ContextFloatKey distanceKey = null;
		[SerializeField, FoldoutGroup("Data")] private float desiredRange = 5f;

		private SharedFloat distance = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			distance = behaviourProcessor.GetData<SharedFloat>(distanceKey);
		}

		public override void ProcessTask()
		{
			float distance = this.distance.Value;

			bool result = distance <= desiredRange;
			ProcessCondition(result);
		}
	}
}
