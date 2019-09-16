using UnityEngine;
using ToolBox.Attributes;

public class ActorAnimator : MonoBehaviour
{
	public bool animationState { get; private set; }

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
		animator.SetInteger(animatorParametr, behaviorProcessor.currentState.stateIndex);
		animationState = false;
	}

	// Called by an animation event
	public void SetAnimationState(bool animationState) => this.animationState = animationState;

	// Called by an animation event
	public void PlayerRandomSubAnimation() => animator.SetFloat(subAnimatorParametr, Random.value);
}
