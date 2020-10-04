using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class StateMachineTests
    {

        void Method1(StateMachine<string, string, string>.State state, Dictionary<string, object> parameters)
        {
            Assert.AreEqual(parameters["Data1"], "Test Data");

            state.StateMachine.InvokeTransition("Transition1");
        }


        void Method2(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition2");
        }
        void Method3(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransition("Transition3");
        }
        void Method4(State state, Dictionary<string, object> parameters)
        {

        }

        void ActionOnTransitionInvoke(StateMachine<string, string, string>.Transition transition, Dictionary<string, object> parameters)
        {

        }
        void ActionOnChangeState(StateMachine<string, string, string>.State stateFrom, StateMachine<string, string, string>.State stateTo)
        {

        }

        static Dictionary<string, object> parametersForStart = new Dictionary<string, object>() { { "Data1", "Test Data" } };
        static Dictionary<string, object> parametersForStart2 = new Dictionary<string, object>() { { "string", "stroka" } };
        [TestCategory("StateMachine")]
        [TestMethod]
        public void StateMachineFromCode()
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
            var logger = loggerFactory.CreateLogger<StateMachine<string, string, string>>();
            var stateMachine = new StateMachine<Guid, Guid, Guid>(logger);
            var state1 = stateMachine.AddState("State1", actionOnExit: Method1);
            var state2 = stateMachine.AddState("State2");
            var state3 = stateMachine.AddState("State3");
            var state4 = stateMachine.AddState("State4");

            Assert.IsTrue(stateMachine.StateExists("State1"));

            stateMachine.OnChangeState(ActionOnChangeState);

            var transition1 = state1.AddTransitionFromThis("Transition1", state2);
            var transition2 = stateMachine.AddTransition("Transition2", state2, state3);
            var transition3 = state4.AddTransitionToThis("Transition3", state3);

            Assert.IsTrue(stateMachine.TransitionExists("Transition1"));

            state1.SetAsStartState();
            state2.OnExit(Method2);
            state3.OnExit(Method3);
            state4.OnExit(Method4);
            stateMachine.AddData("int1", 55);
            stateMachine.AddData("string1", "Roman");
            stateMachine.AddData("double1", 1001.0005);

            Assert.IsTrue(stateMachine.DataExists("int1"));
            stateMachine.Start(parametersForStart);

            Assert.AreEqual(stateMachine.CurrentState.Name, "State4");

            stateMachine.ToXDocument("text.xml");

        }

        //    [TestCategory("StateMachine")]
        //    [TestMethod]
        //    public void StateMachineFromXML()
        //    {
        //        var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
        //        var logger = loggerFactory.CreateLogger<StateMachine>();

        //        StateMachine stateMachine = StateMachine.FromXDocument("text.xml", logger);

        //        stateMachine.GetState("State1").OnExit(Method1);
        //        stateMachine.GetState("State2").OnExit(Method2);
        //        stateMachine.GetState("State3").OnExit(Method3);

        //        stateMachine.OnChangeState(ActionOnChangeState);

        //        stateMachine.Start(parametersForStart);

        //        Assert.AreEqual(stateMachine.CurrentState.Name, "State4");
        //    }

    }
}
