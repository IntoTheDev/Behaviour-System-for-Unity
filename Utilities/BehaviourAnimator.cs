using ToolBox.Attributes;
using UnityEngine;

namespace ToolBox.Behaviours.Utilities
{
	[RequireComponent(typeof(BehaviourProcessor), typeof(Animator)), DisallowMultipleComponent]
	public class BehaviourAnimator : MonoBehaviour
	{
		public bool IsAnimationEnded { get; private set; } = false;

		[SerializeField, BoxGroup("Components")] private Animator animator = null;
		[SerializeField, BoxGroup("Components")] private BehaviourProcessor behaviorProcessor = null;

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
			IsAnimationEnded = false;
		}

		public void SetAnimationState(bool animationState) => IsAnimationEnded = animationState;
	}
}
