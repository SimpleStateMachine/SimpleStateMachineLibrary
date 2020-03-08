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

            state.StateMachine.InvokeTransitionWithParameters("Transition1", new Dictionary<string, object>() { { "Test1", "Test1" } });
        }
        void Method2(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransitionWithParameters("Transition2", new Dictionary<string, object>() { { "Test2", "Test2" } });
        }
        void Method3(State state, Dictionary<string, object> parameters)
        {
            state.StateMachine.InvokeTransitionWithParameters("Transition3", new Dictionary<string, object>() { { "Test2", "Test2" } });
        }
        void MethodOnChange(State stateFrom, State stateTo)
        {

        }
        static Dictionary<string, object> parametersForStart = new Dictionary<string, object>() { { "Data1", "Test Data" } };

        [TestMethod]
        public void StateMachineFromCode()
        {
            StateMachine stateMachine = new StateMachine();

            State state1 = stateMachine.AddState("State1");
            State state2 = stateMachine.AddState("State2");
            State state3 = stateMachine.AddState("State3");
            State state4 = stateMachine.AddState("State4");

            stateMachine.OnChangeState(MethodOnChange);

            Transition transition1 = state1.AddTransitionFromThis("Transition1", state2);
            Transition transition2 = stateMachine.AddTransition("Transition2", state2, state3);
            Transition transition3 = state4.AddTransitionToThis("Transition3", state3);

            state1.SetAsStartState();
            state1.OnExit(Method1);
            state2.OnExit(Method2);
            state3.OnExit(Method3);

            stateMachine.AddData("int1", 55);
            stateMachine.AddData("string1", "Roman");
            stateMachine.AddData("double1", 1001.0005);


            stateMachine.Start(parametersForStart);

            Assert.AreEqual(stateMachine.CurrentState.Name, "State4");

          

            stateMachine.ToXDocument("text.xml");
        }

        [TestMethod]
        public void StateMachineFromXML()
        {
            StateMachine stateMachine = StateMachine.FromXDocument("text.xml");

            stateMachine.State("State1").OnExit(Method1);
            stateMachine.State("State2").OnExit(Method2);
            stateMachine.State("State3").OnExit(Method3);

            stateMachine.OnChangeState(MethodOnChange);

            stateMachine.Start(parametersForStart);

            Assert.AreEqual(stateMachine.CurrentState.Name, "State4");
        }

    }
}
