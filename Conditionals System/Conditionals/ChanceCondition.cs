using UnityEngine;

namespace ToolBox.Behaviours.Conditionals
{
	public class ChanceCondition : Conditional
	{
		[SerializeField] private float chance = 0.5f;

		public override bool Execute() => Extensions.Extensions.PercentChance(chance);
	}
}