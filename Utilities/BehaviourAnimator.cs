using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours.Utilities
{
	[RequireComponent(typeof(Animator)), DisallowMultipleComponent]
	public class BehaviourAnimator : MonoBehaviour
	{
		[SerializeField, FoldoutGroup("Data"), ListDrawerSettings(NumberOfItemsPerPage = 1)] private AnimationEvents[] animationEvents = null;

		[SerializeField, FoldoutGroup("Components"), Required] private Animator animator = null;
		[SerializeField, FoldoutGroup("Components"), Required] private BehaviourProcessor behaviorProcessor = null;

		private int animatorParametr = 0;
		private int subAnimatorParametr = 0;

		private void Start()
		{
			animatorParametr = Animator.StringToHash("State");
			subAnimatorParametr = Animator.StringToHash("Blend");

			behaviorProcessor.OnStateChange += UpdateAnimator;
			enabled = behaviorProcessor.enabled == true && behaviorProcessor != null;
		}

		private void UpdateAnimator(State state)
		{
			animator.SetFloat(subAnimatorParametr, Random.value);
			animator.SetInteger(animatorParametr, state.Index);
		}

		public void CallEvents(int index) => animationEvents[index].Events?.Invoke();

		[System.Serializable]
		private struct AnimationEvents
		{
			public UnityEvent Events => events;


#if UNITY_EDITOR
			[SerializeField] private string editorName;
#endif

			[SerializeField] private UnityEvent events;
		}
	}
}
