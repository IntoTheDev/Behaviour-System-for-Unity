using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Actions
{
	public class DistanceToTarget : Action
	{
		[SerializeField, FoldoutGroup("Data")] private ContextFloatKey distanceKey = null;
		[SerializeField, FoldoutGroup("Data")] private ContextTransformKey targetKey = null;

		private SharedFloat distance = null;
		private SharedTransform target = null;

		public override void Initialize(BehaviourProcessor behaviourProcessor)
		{
			base.Initialize(behaviourProcessor);

			distance = behaviourProcessor.GetData<SharedFloat>(distanceKey);
			target = behaviourProcessor.GetData<SharedTransform>(targetKey);
		}

		public override void ProcessTask() =>
			distance.Value = Vector2.Distance(cachedTransform.position, target.Value.position);
	}
}
