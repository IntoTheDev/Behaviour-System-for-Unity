using UnityEngine;
using ToolBox.Attributes;

public class BehaviourProcessor : MonoBehaviour
{
	// Needed for other systems
	public delegate void OnStateChange();
	public event OnStateChange onStateChange;

	[ReadOnly, BoxGroup("Debug")] public State currentState = null;

	private int statesCount;

	[ReorderableList, SerializeField, BoxGroup("States")] private State[] states;
	[SerializeField, BoxGroup("Debug")] private bool aiActive;

	private void Start()
	{
		statesCount = states.Length;
		InitializeStates();
		TransitionToState(states[0].stateName);
	}

	private void Update()
	{
		if (aiActive)
			currentState.UpdateState();
	}

	public void TransitionToState(string nextState)
	{
		if (nextState == currentState.stateName)
			return;

		if (currentState != null)
			currentState.ExitState();

		currentState = FindState(nextState);

		currentState.EnterState();

		onStateChange();
	}

	private State FindState(string newStateName)
	{
		for (int i = 0; i < statesCount; i++)
			if (newStateName == states[i].stateName)
				return states[i];

		return null;
	}

	private void InitializeStates()
	{
		if (states[0] == null)
		{
			aiActive = false;
			Debug.LogError(name + " doesn't have states!");
			enabled = false;
			return;
		}

		for (int i = 0; i < statesCount; i++)
			states[i].InitializeState(this, i);
	}
}
