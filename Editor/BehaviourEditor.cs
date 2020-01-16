using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace ToolBox.Behaviours.Editor
{
	public class BehaviourEditor : OdinEditorWindow
	{
		[MenuItem("Window/ToolBox/Behaviour")]
		private static void OpenWindow() =>
			GetWindow<BehaviourEditor>().Show();

		protected override object GetTarget() =>
			Selection.activeGameObject?.GetComponent<BehaviourProcessor>();
	}
}
