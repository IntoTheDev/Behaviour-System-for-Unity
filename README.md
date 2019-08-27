# BehaviourSystem
Universal behaviour system for all kinds of entities.

Files usage:

BehaviourProcessor - main script that will contain all the States of the object.

State - state of an entity that contains actions and transitions.

Action - actions that the entity performs

Decision - depending on the decisions, the entity will pass to another state.

Transition - in the transitions stored decision and states of two types. True States into which an entity can pass if the decisions are true. False States to which an entity can pass if the decisions are false.

Also i'm using NaughtyAttributes for some attributes.





