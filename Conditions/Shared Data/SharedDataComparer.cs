using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Conditions
{
	public abstract class SharedDataComparer<T, C> where C : ContextKey
	{
		[HideInInspector] public event UnityAction<bool> OnValueChanged = null;

		protected SharedData<T, C> sharedDataToCompare = null;
		protected EqualityComparer<T> equalityComparer = null;

		public virtual void Initialize(ContextKey contextKey, BehaviourProcessor behaviourProcessor)
		{
			sharedDataToCompare = behaviourProcessor.GetData<SharedData<T, C>>(contextKey);
			equalityComparer = EqualityComparer<T>.Default;
		}

		public abstract void OnEnter();

		public abstract void OnExit();

		public void Invoke(bool result) =>
			OnValueChanged?.Invoke(result);
	}
}
