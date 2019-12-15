using Sirenix.OdinInspector;
using UnityEngine;

namespace ToolBox.Behaviours.UtilityAI
{
	public abstract class Scorer
	{
		public bool IsNot => isNot;
		public float Score => score;

		[SerializeField, FoldoutGroup("Scorer Data")] private bool isNot = false;
		[SerializeField, FoldoutGroup("Scorer Data")] private float score = 0f;

		public abstract bool Execute();
	}
}
