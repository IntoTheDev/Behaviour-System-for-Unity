using System;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	public abstract class SharedData
	{
		[HideInInspector] public UnityAction OnValueChanged = null;

		public abstract ContextKey GetKey();

		public abstract void SetValue(object value);

		public abstract object GetValue();

		public abstract void SetValueFromSharedData(SharedData sharedData);

		public abstract bool IsValueEquals(SharedData sharedData);

	}

	public interface ISharedData<T>
	{
		T Value { get; set; }
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

		public override void SetValue(object value) =>
			Value = (T)value;

		public override object GetValue() =>
			value;

		public override void SetValueFromSharedData(SharedData sharedData) =>
			Value = (T)sharedData.GetValue();

		public override bool IsValueEquals(SharedData sharedData) =>
			value.Equals(sharedData.GetValue());
	}
}
