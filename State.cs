using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[System.Serializable]
	public class State
	{
		public int Index => index;

#if UNITY_EDITOR
		[SerializeField] private string debugName = "";
#endif
		[SerializeField, ReadOnly] private int index = 0;

		[SerializeField] private UnityEvent enterEvents = null;
		[SerializeField] private UnityEvent exitEvents = null;

		public void EnterState() => enterEvents?.Invoke();

		public void ExitState() => exitEvents?.Invoke();

		public void SetIndex(int index) => this.index = index;
	}
}
