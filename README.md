[![NuGet Downloads](https://img.shields.io/nuget/dt/SimpleStateMachineLibrary)](https://www.nuget.org/packages/SimpleStateMachineLibrary)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/SimpleStateMachineLibrary.svg)](https://www.nuget.org/packages/SimpleStateMachineLibrary) 
[![](https://img.shields.io/github/stars/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) 
[![](https://img.shields.io/github/license/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) 
# SimpleStateMachineLibrary 
A C# library for realization simple state-machine on .Net

[📄Documentation](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki)
# Why SimpleStateMachine?
 Create state machine in **two steps** :
1. Create scheme in  [node editor](https://github.com/SimpleStateMachine/SimpleStateMachineNodeEditor) ♦️
2. Load scheme in your project using [library](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary)📑
 
 Just describe your app logic and run the state machine🚀
 
## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!
## Сontent
1. [Features](#Features)
2. [Examples](#Examples)
4. [Documentation](#Documentation)
4. [License](#License)

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


## Examples:

### Structure ###
```C#
            StateMachine stateMachine = new StateMachine();

            //Add states
            State state1 = stateMachine.AddState("State1");
            State state2 = stateMachine.AddState("State2");
            State state3 = stateMachine.AddState("State3");
            State state4 = stateMachine.AddState("State4");

            //Add transitions three ways:

            //Standart way
            Transition transition1 = stateMachine.AddTransition("Transition1", state1, state2);

            //From state
            Transition transition2 = state2.AddTransitionFromThis("Transition2", state3);

            //To state
            Transition transition3 = state4.AddTransitionToThis("Transition3", state3);
          
            //Add action on entry or/and exit
            state1.OnExit(Action1);
            state2.OnEntry(Action2);
            state3.OnExit(Action3);
            state4.OnExit(Action4);

            //Set start state
            state1.SetAsStartState();

            //Start work
            stateMachine.Start();
```
### Actions Syntax ###
##### Action on entry/exit #####
```C#
        void ActionOnEtnry(State state, Dictionary<string, object> parameters)
        {
            //you need invoke transition in entry or exit action, differently work state machine will be end
            state.StateMachine.InvokeTransition("Transition1");
        }

```
##### Action on change state #####
```C#
        void ActionOnChangeState(State stateFrom, State stateTo)
        {

        }
```
##### Action on transition invoke #####
```C#
        void ActionOnTransitionInvoke(Transition transition, Dictionary<string, object> parameters)
        {

        }
```
## Documentation
* StateMachine
    * [Create](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#Create)
    * [Import](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#Import)
    * [Export](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#Export)
    * [Logging](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#Logging)
    * [OnChangeState](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#OnChangeState)
    * [CurrentState](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#CurrentState)
    * [PreviousState](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#PreviousState)
    * [CurrentTransition](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/StateMachine#CurrentTransition)
* State
    * [Create](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Create)
    * [Get](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Get)
    * [Exists](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Exists)
    * [Delete](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Delete)
    * [Entry](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Entry)
    * [Exit](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/State#Exit)
* Transition
    * [Create](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Create)
    * [Get](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Get)
    * [Exists](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Exists)
    * [Delete](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Delete)
    * [Invoke](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Invoke)
    * [Parameters](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Transition#Parameters)
* Data
    * [Create](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Data#Create)
    * [Get](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Data#Get)
    * [Exists](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Data#Exists)
    * [Delete](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Data#Delete)
    * [Change](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki/Data#Change)
## License

Copyright (c) SimpleStateMachine

Licensed under the [MIT](LICENSE) license.

