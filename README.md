# Behaviour System for Unity
Hierarchical Finite State Machine for Unity.

Pros:
- It is much easier to work with it than with a default state machine
- Highly optimized
- Modular
- Different variations of task execution (All Update types and also Slow Update for heavy tasks, Every 'X' Second, On State Enter, on State Exit, Delayed)
- Sequence and Selector for Conditions almost like in Behaviour Trees
- There are UnityEvents for: Behaviour OnEnter/OnExit, State OnEnter/OnExit, Conditions OnSuccess/OnFailure

Cons:
- Bad visual, not node based, but everything can be setup within inspector
- Requires "Odin - Inspector and Serializer" (Cost is 55$)
- Requires MEC (Free)
