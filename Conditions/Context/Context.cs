using MEC;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using ToolBox.Behaviours.Composites;
using ToolBox.Behaviours.Utilities;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class Context : Condition
	{
		[SerializeField, FoldoutGroup("Data")] protected ContextKey contextKey = null;
		[SerializeField, FoldoutGroup("Data")] protected UpdateRate updateRate = default;

		protected SharedData<object> sharedData = null;

		private CoroutineHandle coroutineHandle = default;
		private float time = 0f;
		private GameObject cachedObject = null;

		public override void Initialize(Composite composite, BehaviourProcessor behaviour)
		{
			base.Initialize(composite, behaviour);

			time = updateRate.GetRate();
			sharedData = behaviour.GetData(contextKey);
			cachedObject = behaviour.gameObject;
		}

		public override void OnEnter()
		{
			Process();
			coroutineHandle = Timing.RunCoroutine(CallRepeating().CancelWith(cachedObject));
		}

		public override void OnExit() =>
			Timing.KillCoroutines(coroutineHandle);

		public abstract void Process();

		private IEnumerator<float> CallRepeating()
		{
			while (true)
			{
				Process();
				yield return Timing.WaitForSeconds(time);
			}
		}
	}
}
