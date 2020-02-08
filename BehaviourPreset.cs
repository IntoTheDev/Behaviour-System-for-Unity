using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ToolBox.Behaviours
{
    [CreateAssetMenu(menuName = "ToolBox/Behaviours/Preset")]
    public class BehaviourPreset : SerializedScriptableObject
    {
        [OdinSerialize] public Behaviour Behaviour = null;
    }
}
