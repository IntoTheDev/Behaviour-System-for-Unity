using UnityEngine;

namespace ToolBox.Behaviours
{
	[System.Serializable]
	public class State
	{
		public string StateName => stateName;
		public int StateIndex { get; private set; }

		[SerializeField] private string stateName = "";
		[SerializeField] private Action[] actions = null;
		[SerializeField] private Transition[] transitions = null;

		private State[][] trueStates = null;
		private State[][] falseStates = null;

		private BehaviourProcessor behaviour;
		private int actionsCount;
		private int transitionsCount;
		private int[] decisionsCount;

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

				State[] possibleStates = decisionSucceeded ? trueStates[i] : falseStates[i];

				behaviour.TransitionToState(GetState(possibleStates));

				if (NotThisState())
					break;
			}
		}

		private State GetState(State[] stateCollection)
		{
			int statesLength = stateCollection.Length;

			if (statesLength == 1)
			{
				return stateCollection[0];
			}
			else if (statesLength > 1)
			{
				int randomIndex = Random.Range(0, statesLength - 1);
				return stateCollection[randomIndex];
			}
			else
			{
				return null;
			}
		}

		public void InitializeState(BehaviourProcessor behaviourProcessor, int stateIndex)
		{
			// Get Behaviour processor
			behaviour = behaviourProcessor;
			StateIndex = stateIndex;

			// Get actions and transitions length
			actionsCount = actions.Length;
			transitionsCount = transitions.Length;

			// Check if there are any actions
			if (actionsCount <= 0 || actions[0] == null)
			{
				behaviour.enabled = false;
				Debug.LogError(behaviour.name + "doesn't have Actions in state: " + stateName);
			}

			// Check if there are any transitions
			if (transitionsCount <= 0 || transitions[0].decision == null)
			{
				behaviour.enabled = false;
				Debug.LogError(behaviour.name + "doesn't have Transitions in state: " + stateName);
			}

			// Get decisions length
			decisionsCount = new int[transitionsCount];

			for (int i = 0; i < transitionsCount; i++)
				decisionsCount[i] = transitions[i].decision.Length;

			// Cache all possible states
			trueStates = new State[transitionsCount][];
			falseStates = new State[transitionsCount][];

			for (int i = 0; i < transitionsCount; i++)
			{
				int trueStatesLength = transitions[i].trueStates.Length;
				int falseStatesLength = transitions[i].falseStates.Length;

				State[] cachedTrueStates = new State[trueStatesLength];
				State[] cachedFalseStates = new State[falseStatesLength];

				for (int j = 0; j < trueStatesLength; j++)
					cachedTrueStates[j] = behaviour.FindState(transitions[i].trueStates[j]);

				for (int j = 0; j < falseStatesLength; j++)
					cachedFalseStates[j] = behaviour.FindState(transitions[i].falseStates[j]);

				trueStates[i] = cachedTrueStates;
				falseStates[i] = cachedFalseStates;
			}
		}

		private bool NotThisState()
		{
			if (behaviour.currentState.StateIndex != StateIndex)
				return true;
			else
				return false;
		}
	}
}
