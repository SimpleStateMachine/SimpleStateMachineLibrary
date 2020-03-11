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

        static Dictionary<string, object> parametersForStart = new Dictionary<string, object>() { { "Data1", "Test Data" } };

        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
            var logger = loggerFactory.CreateLogger<StateMachine>();
            StateMachine stateMachine = new StateMachine(logger);

            State state1 = stateMachine.AddState("State1");
            State state2 = stateMachine.AddState("State2");
            State state3 = stateMachine.AddState("State3");
            State state4 = stateMachine.AddState("State4");

            Transition transition1 = state1.AddTransitionFromThis("Transition1", state2);
            Transition transition2 = stateMachine.AddTransition("Transition2", state2, state3);
            Transition transition3 = state4.AddTransitionToThis("Transition3", state3);

            state1.SetAsStartState();
            state1.OnExit(Method1);
            state2.OnExit(Method2);
            state3.OnExit(Method3);
            state4.OnExit(Method4);

            stateMachine.AddData("int1", 55);
            stateMachine.AddData("string1", "Roman");
            stateMachine.AddData("double1", 1001.0005);

            stateMachine.Start(parametersForStart);

            stateMachine.ToXDocument("text.xml");
        }
    }
}
