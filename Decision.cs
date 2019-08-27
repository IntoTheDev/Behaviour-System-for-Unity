using UnityEngine;

public abstract class Decision : MonoBehaviour
{
	protected BehaviorProcessor behavior;

	public void InitializeDecision(BehaviorProcessor behaviorProcessor)
	{
		behavior = behaviorProcessor;
	}

	public abstract void EnterDecision();

	public abstract bool Decide();

	public abstract void ExitDecision();
}