using ToolBox.Attributes;
using ToolBox.Behaviours;
using UnityEngine;

public class ActorAnimator : MonoBehaviour
{
	public bool IsAnimationEnded { get; private set; }

	[SerializeField, BoxGroup("Components")] private Animator animator = null;
	[SerializeField, BoxGroup("Components")] private BehaviourProcessor behaviorProcessor = null;

	private int animatorParametr = 0;
	private int subAnimatorParametr = 0;

	private void Start()
	{
		animatorParametr = Animator.StringToHash("State");
		subAnimatorParametr = Animator.StringToHash("Blend");

		behaviorProcessor.onStateChange += UpdateAnimator;
		enabled = behaviorProcessor.enabled == true && behaviorProcessor != null;
	}

	private void UpdateAnimator()
	{
		animator.SetFloat(subAnimatorParametr, Random.value);
		animator.SetInteger(animatorParametr, behaviorProcessor.currentState.StateIndex);
		IsAnimationEnded = false;
	}

	// Called by an animation event
	public void SetAnimationState(bool animationState) => IsAnimationEnded = animationState;
}
