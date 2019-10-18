using ToolBox.Editor;
using UnityEditor;

namespace ToolBox.Behaviours.Editor
{
	public class BehaviourEditor : ToolBoxEditor
	{
		[MenuItem("Assets/Create/ToolBox/Behaviours/Action")]
		public static void CreateAction()
		{
			string path = "Assets/Scripts/ToolBox/Behaviour System/Editor/Templates/ActionTemplate.cs";
			string newPath = "Assets/Scripts/ToolBox/Behaviour System/Actions/NewAction.cs";

			CreateAsset(path, newPath);
		}

		[MenuItem("Assets/Create/ToolBox/Behaviours/Decision")]
		public static void CreateDecision()
		{
			string path = "Assets/Scripts/ToolBox/Behaviour System/Editor/Templates/DecisionTemplate.cs";
			string newPath = "Assets/Scripts/ToolBox/Behaviour System/Decisions/NewDecision.cs";

			CreateAsset(path, newPath);
		}
	}
}

