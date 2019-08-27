using UnityEngine;

public abstract class Action : MonoBehaviour
{
	protected BehaviorProcessor behavior;

	public void InitializeAction(BehaviorProcessor behaviorProcessor)
	{
		behavior = behaviorProcessor;
	}

	public abstract void EnterAction();

	public abstract void Act();

	public abstract void ExitAction();
}