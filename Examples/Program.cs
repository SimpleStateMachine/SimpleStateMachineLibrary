using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary;

namespace Examples
{
    class Program
    {
        static void Method1(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition1");
        }
        static  void Method2(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition2");
        }
        static void Method3(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition3");
        }
        static void Method4(State state, Dictionary<string, object> parameters)
        {

        }

        static void Main(string[] args)
        {
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
            state1.OnExit(Method1);
            state2.OnEntry(Method2);
            state3.OnExit(Method3);
            state4.OnExit(Method4);

            //Set start state
            state1.SetAsStartState();

            //Start work
            stateMachine.Start();
        }
    }
}
