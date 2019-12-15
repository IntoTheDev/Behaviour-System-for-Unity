using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.UtilityAI
{
	public class ChanceScorer : Scorer
	{
		[SerializeField, FoldoutGroup("Component Data"), Range(0f, 1f)] private float chance = 0.5f;

		public override bool Execute() => Extensions.Extensions.PercentChance(chance);
	}
}