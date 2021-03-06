﻿using Sirenix.OdinInspector;
using ToolBox.Behaviours.Actions;
using ToolBox.Behaviours.Composites;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Behaviours
{
	[System.Serializable]
	public class State
	{
#if UNITY_EDITOR
		public string StateName => stateName;

		[SerializeField] private string stateName = "State";
#endif

		[SerializeField, FoldoutGroup("Events")] private UnityEvent onEnter = null;
		[SerializeField, FoldoutGroup("Events")] private UnityEvent onExit = null;

		[SerializeField, FoldoutGroup("Composites"), ListDrawerSettings(
			NumberOfItemsPerPage = 1,
			Expanded = true,
			DraggableItems = false)] private Composite[] composites = null;

		[SerializeField, FoldoutGroup("Actions"), ListDrawerSettings(
			NumberOfItemsPerPage = 1,
			Expanded = true,
			DraggableItems = false)] private Action[] actions = null;

		private BehaviourProcessor behaviourProcessor = null;

		public void Initialize(BehaviourProcessor behaviour)
		{
			behaviourProcessor = behaviour;

			for (int i = 0; i < composites.Length; i++)
				composites[i].Initialize(behaviour);

			for (int i = 0; i < actions.Length; i++)
				actions[i].Initialize(behaviour);
		}

		public void OnEnter()
		{
			onEnter?.Invoke();

			for (int i = 0; i < composites.Length; i++)
				composites[i].OnEnter();

			for (int i = 0; i < actions.Length; i++)
				actions[i].OnEnter();

			behaviourProcessor.ToggleTasks();
		}

		public void OnExit()
		{
			onExit?.Invoke();

			for (int i = 0; i < composites.Length; i++)
				composites[i].OnExit();

			for (int i = 0; i < actions.Length; i++)
				actions[i].OnExit();
		}

		public T GetAction<T>() where T : Action
		{
			for (int i = 0; i < actions.Length; i++)
			{
				Action action = actions[i];
				object type = action.GetType();

				if (type.Equals(typeof(T)))
					return action as T;
			}

			return null;
		}
	}
}
