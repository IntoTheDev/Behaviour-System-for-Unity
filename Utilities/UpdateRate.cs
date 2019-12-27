using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Utilities
{
	[System.Serializable]
	public struct UpdateRate
	{
		[SerializeField, HideIf("once")] private float value;
		[SerializeField] private bool once;

		public float GetRate()
		{
			float rate = once ? float.MaxValue : value;
			return rate;
		}
	}

}
