
 [![NuGet Downloads](https://img.shields.io/nuget/dt/SimpleStateMachineLibrary)](https://www.nuget.org/packages/SimpleStateMachineLibrary) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/SimpleStateMachineLibrary.svg)](https://www.nuget.org/packages/SimpleStateMachineLibrary) [![](https://img.shields.io/github/stars/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) [![](https://img.shields.io/github/license/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) [![](https://img.shields.io/github/languages/code-size/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) 
 [![]( https://img.shields.io/github/last-commit/SimpleStateMachine/SimpleStateMachineLibrary)](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary) [![](https://img.shields.io/badge/chat-slack-blueviolet.svg)](https://join.slack.com/t/simplestatemachine/shared_invite/zt-fnfhvvsx-fTejcpPn~PPb2ojdG_MQBg) [![](https://img.shields.io/badge/chat-telegram-blue.svg)](https://t.me/joinchat/HMLJFkv9do6aDV188rhd0w)
 # SimpleStateMachineLibrary
A C# library for realization simple state-machine on .Net

 ## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

 # Why SimpleStateMachine?
Create state machine in **three** steps :

**1.** Create scheme in  [node editor🔗](https://github.com/SimpleStateMachine/SimpleStateMachineNodeEditor) and load it in your project using [this library📚](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary)
```C#
StateMachine stateMachine = new StateMachine("scheme.xml");
```
**2.** Describe your app logic on events⚡
 ```C#
stateMachine.GetState("State1").OnExit(Action1);
stateMachine.GetState("State2").OnEntry(Action2);
stateMachine.GetTransition("Transition1").OnInvoke(Action3);
stateMachine.OnChangeState(Action4);
```
**3.** Run the state machine🚘
 ```C#
stateMachine.Start();
```
## Features💡 

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

## Getting Started📂
Install from Nuget:
```sh
 Install-Package SimpleStateMachineLibrary 
```
## Documentation📄 
Documentation here: https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/wiki

 ## FAQ❔
 If you think you have found a bug, create a github [issue](https://github.com/SimpleStateMachine/SimpleStateMachineLibrary/issues).
 
 But if you just have questions about how to use:
 
- [Slack channel](https://join.slack.com/t/simplestatemachine/shared_invite/zt-fnfhvvsx-fTejcpPn~PPb2ojdG_MQBg)
- [Telegram channel](https://t.me/joinchat/HMLJFkv9do6aDV188rhd0w)

## License📑

Copyright (c) SimpleStateMachine

Licensed under the [MIT](LICENSE) license.

