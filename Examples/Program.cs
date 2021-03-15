using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;

namespace Examples
{
    class Program
    {
        static void Action1(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition1");
        }
        static void Action2(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition2");
        }
        static void Action3(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition3");
        }
        static void Action4(State state, Dictionary<string, object> parameters)
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


            state1.Delete();
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
        }
    }
}
