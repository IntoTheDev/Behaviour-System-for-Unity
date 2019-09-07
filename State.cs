using UnityEngine;

[System.Serializable]
public class State
{
	public string stateName;

	[HideInInspector] public int stateIndex;

	private BehaviourProcessor behavior;
	private int actionsCount;
	private int transitionsCount;
	private int[] decisionsCount;

	[SerializeField] private Action[] actions;
	[SerializeField] private Transition[] transitions;

	public void EnterState()
	{
		for (int i = 0; i < actionsCount; i++)
			actions[i].EnterAction();

		for (int i = 0; i < transitionsCount; i++)
			for (int j = 0; j < decisionsCount[i]; j++)
				transitions[i].decision[j].EnterDecision();
	}

	public void UpdateState()
	{
		PerformActions();
		PerformTransitions();
	}

	public void ExitState()
	{
		for (int i = 0; i < actionsCount; i++)
			actions[i].ExitAction();

		for (int i = 0; i < transitionsCount; i++)
			for (int j = 0; j < decisionsCount[i]; j++)
				transitions[i].decision[j].ExitDecision();
	}

	private void PerformActions()
	{
		for (int i = 0; i < actionsCount; i++)
			actions[i].Act();
	}

	private void PerformTransitions()
	{
		for (int i = 0; i < transitionsCount; i++)
		{
			bool decisionSucceeded = true;

			for (int j = 0; j < decisionsCount[i]; j++)
			{
				bool currentDecision = transitions[i].decision[j].Decide();

				decisionSucceeded = currentDecision && decisionSucceeded;
			}

			string[] newStates = decisionSucceeded ? transitions[i].trueStates : transitions[i].falseStates;
			behavior.TransitionToState(GetState(newStates));

			if (NotThisState())
				break;
		}
	}

	private string GetState(string[] stateCollection)
	{
		int stateLength = stateCollection.Length;

		if (stateLength == 1)
		{
			return stateCollection[0];
		}
		else if (stateLength > 1)
		{
			int randomIndex = Random.Range(0, stateLength - 1);
			return stateCollection[randomIndex];
		}
		else
		{
			return null;
		}
	}

	public void InitializeState(BehaviourProcessor behaviorProcessor, int stateIndex)
	{
		behavior = behaviorProcessor;
		this.stateIndex = stateIndex;

		actionsCount = actions.Length;
		transitionsCount = transitions.Length;

		if (actionsCount <= 0 || actions[0] == null)
			return;

		if (transitionsCount <= 0 || transitions[0].decision == null)
			return;

		decisionsCount = new int[transitionsCount];

		for (int i = 0; i < transitionsCount; i++)
			decisionsCount[i] = transitions[i].decision.Length;
	}

	private bool NotThisState()
	{
		if (behavior.currentState.stateName != stateName)
			return true;
		else
			return false;
	}
}