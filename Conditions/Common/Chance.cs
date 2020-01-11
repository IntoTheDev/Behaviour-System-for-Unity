using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.Conditions
{
	public class Chance : Condition
	{
		[SerializeField, Range(0f, 1f), FoldoutGroup("Data")] private float chance = 0.5f;

		public override void ProcessTask()
		{
			bool result = Extensions.Extensions.PercentChance(chance);
			ProcessCondition(result);
		}
	}
}
