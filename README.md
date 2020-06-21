
# SimpleStateMachineLibrary [![NuGet Downloads](https://img.shields.io/nuget/dt/SimpleStateMachineLibrary)](https://www.nuget.org/packages/SimpleStateMachineLibrary) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/SimpleStateMachineLibrary.svg)](https://www.nuget.org/packages/SimpleStateMachineLibrary) [![](https://img.shields.io/github/stars/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) [![](https://img.shields.io/github/license/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) 
A C# library for realization simple state-machine on .Net

 # Why SimpleStateMachine?
Create state machine in **three** steps :

**1.** Create scheme in  [node editor](https://github.com/SimpleStateMachine/SimpleStateMachineNodeEditor) and load it in your project using [this library 📚](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary)
```C#
StateMachine stateMachine = new StateMachine("scheme.xml");
```
**2.** Describe your app logic on events
 ```C#
stateMachine.GetState("State1").OnExit(Action1);
stateMachine.GetState("State2").OnEntry(Action2);
stateMachine.GetTransition("Transition1").OnInvoke(Action3);
stateMachine.OnChangeState(Action4);
```
**3.** Run the state machine
 ```C#
stateMachine.Start();
```
## Features

State machine properties:
* Start state
* Entry/exit events for state
* Invoke event for transition
* Parameters for transitions
* Parameters for entry/exit for state

Useful extensions for work:
* State changed event for state machine
* Data for sharing between states
* Change event for data
* Export/Import to/from XML
* Logging

## Documentation
Documentation here: https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki

## License

Copyright (c) SimpleStateMachine

Licensed under the [MIT](LICENSE) license.

