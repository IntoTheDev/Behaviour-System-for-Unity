using UnityEngine;

namespace ToolBox.Behaviours
{
	public abstract class Action : MonoBehaviour
	{
		public abstract void EnterAction();

		public abstract void Act();

		public abstract void ExitAction();
	}
}
