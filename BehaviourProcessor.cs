using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class BehaviourProcessor : MonoBehaviour
{
	[ReorderableList, SerializeField, BoxGroup("States")]
	private List<State> states;
	private int statesCount;
	[ReadOnly, BoxGroup("Debug")]
	public State currentState = null;

	[SerializeField, BoxGroup("Debug")]
	private bool aiActive;

	// Needed for other systems
	public delegate void OnStateChange();
	public event OnStateChange onStateChange;

	private void Start()
	{
		statesCount = states.Count;
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
