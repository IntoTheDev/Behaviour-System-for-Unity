﻿using UnityEngine;

public abstract class Decision : MonoBehaviour
{
	public abstract void EnterDecision();

	public abstract bool Decide();

	public abstract void ExitDecision();
}