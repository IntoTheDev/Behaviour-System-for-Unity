[System.Serializable]
public struct Transition
{
	public Decision[] decision;
	public string[] trueStates;
	public string[] falseStates;
}