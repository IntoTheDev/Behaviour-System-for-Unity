using System;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	public abstract class SharedData
	{
		[HideInInspector] public UnityAction OnValueChanged = null;

		public abstract ContextKey GetKey();

		public abstract object GetValue();

	}

	public abstract class SharedData<T, C> : SharedData where C : ContextKey
	{
		public C ContextKey => contextKey;
		public T Value
		{
			get
			{
				return value;
			}

			set
			{
				this.value = value;
				OnValueChanged?.Invoke();
			}
		}

		[SerializeField] private C contextKey = null;
		[SerializeField] private T value = default;

		public override ContextKey GetKey() =>
			contextKey;

		public override object GetValue() =>
			value;
	}
}
