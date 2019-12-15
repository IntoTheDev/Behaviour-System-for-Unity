using UnityEngine;

namespace ToolBox.Behaviours.Conditionals
{
	public abstract class Conditional
	{
		public bool IsNot => isNot;

		[SerializeField] private bool isNot = false;

		public abstract bool Execute();
	}
}
