# Simple Scriptable Object State Machine

## Setup
1. Create a machine
2. Create at least one state and set it to default.
3. Add a State Machine Initializer to either a loader scene, or whatever uses the state machine. 
4. Reference the state assets directly and subscribe to their events OR implement IStateListener and register self with state OR extend the provided empty base classes.

## Features
- State Sub-Assets are scriptableObjects, they can be referenced directly.
- Machine uses stack interface to return to previous state.

## Not Included
This machine does not include transitions. It does NOT check if it should switch states according to some set of conditions (like how Unity's animator works).

The state machine lives in a scriptableObject, and there can only be one. There is not a monobehaviour/runtime isntance of the machine that can be repeated. Consider, it could not be used for AI if you are spawning in multiple enemies, as they would all share the same state.
This is not a limitation, but a design decision. Such a system can use a different implemetation of the state machine pattern. This setup is designed to be used for more global elements, things like game state, menu logic, or player input status.

## Other Projects
A few years ago I created [SOStateMachine](https://github.com/hunterdyar/SOStateMachine/), which is the same project. This one uses sub-assets for states, and is much simpler: no transitions or conditions, the flow logic must be implemented elsewhere. I found myself not using that more complex project because it was too much for my use cases.

This project is an attempt at adding some convenience and ergonomics to a pretty simple state system need.
