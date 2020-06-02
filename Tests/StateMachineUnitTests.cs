using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class StateMachineUnitTests
    {
        void Method1(State state, Dictionary<string, object> parameters)
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

        void ActionOnTransitionInvoke(Transition transition, Dictionary<string, object> parameters)
        {

        }
        void ActionOnChangeState(State stateFrom, State stateTo)
        {

        }
        static Dictionary<string, object> parametersForStart = new Dictionary<string, object>() { { "Data1", "Test Data" } };

        [TestMethod]
        public void StateMachineFromCode()
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
            var logger = loggerFactory.CreateLogger<StateMachine>();
            StateMachine stateMachine = new StateMachine(logger);

            State state1 = stateMachine.AddState("State1", actionOnExit: Method1);
            State state2 = stateMachine.AddState("State2");
            State state3 = stateMachine.AddState("State3");
            State state4 = stateMachine.AddState("State4");

            Assert.IsTrue(stateMachine.StateExists("State1"));

            stateMachine.OnChangeState(ActionOnChangeState);

            Transition transition1 = state1.AddTransitionFromThis("Transition1", state2);
            Transition transition2 = stateMachine.AddTransition("Transition2", state2, state3);
            Transition transition3 = state4.AddTransitionToThis("Transition3", state3);

            Assert.IsTrue(stateMachine.TransitionExists("Transition1"));

            state1.SetAsStartState();
            state1.OnExit(Method1);
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

        [TestMethod]
        public void StateMachineFromXML()
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
            var logger = loggerFactory.CreateLogger<StateMachine>();



            StateMachine stateMachine = StateMachine.FromXDocument("text.xml", logger);

            stateMachine.GetState("State1").OnExit(Method1);
            stateMachine.GetState("State2").OnExit(Method2);
            stateMachine.GetState("State3").OnExit(Method3);

            stateMachine.OnChangeState(ActionOnChangeState);

            stateMachine.Start(parametersForStart);

            Assert.AreEqual(stateMachine.CurrentState.Name, "State4");
        }

    }
}
