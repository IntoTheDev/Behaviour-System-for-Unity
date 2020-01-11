# Behaviour System for Unity
Hierarchical Finite State Machine for Unity.

Pros:
- It is much easier and less messy to work with it than with a default state machine
- Highly optimized
- Modular
- Different variations of task execution (All Update types and also Slow Update for heavy tasks, Every 'X' Second, On State Enter, On State Exit, Delayed)
- Sequence and Selector for Conditions almost like in Behaviour Trees
- UnityEvents: Behaviour OnEnter/OnExit, State OnEnter/OnExit, Conditions OnSuccess/OnFailure
- Conditions invertation with one click (For example: ChanceCondition with chance of 100% will return False if "isNot" Field in the Inspector equals to True)
- Methods for return to previous Behaviour/State
- Context (Simillar to Blackboard in UE4 or Shared Variables in Behaviour Designer)

Cons:
- Bad visual, not node based, but everything can be setup within inspector or Custom Editor Window
- Requires "Odin - Inspector and Serializer" (Cost is 55$)
- Requires MEC (Free)
