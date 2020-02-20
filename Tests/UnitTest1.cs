using Xunit;
using System.Collections.Generic;
using SimpleStateMachineLibrary;

namespace Tests
{
    public class UnitTest1
    {
        public struct Test
        {
            public string Name;
            public int Age;
        }

        void Testi1(StateMachine stateMachine, Dictionary<string, object> parameters)
        {
            stateMachine.InvokeTransitionWithParameters("Transition1", new Dictionary<string, object>() { { "Test1", "Test1" } });
        }
        void Testi2(StateMachine stateMachine, Dictionary<string, object> parameters)
        {
            stateMachine.InvokeTransitionWithParameters("Transition2", new Dictionary<string, object>() { { "Test2", "Test2" } });
        }
        [Fact]
        public void Test1()
        {
            StateMachine stateMachine = new StateMachine();
            State state1 = stateMachine.AddState("State1");
            State state2 = stateMachine.AddState("State2");
            State state3 = stateMachine.AddState("State3");
            Transition transition1 = state1.AddTransitionFromThis("Transition1", state2);
            Transition transition2 = state2.AddTransitionFromThis("Transition2", state3);

            state1.SetAsStartState();
            state1.OnExit(Testi1);
            state2.OnExit(Testi1);
            state3.SetAsEndState();

            stateMachine.Start(new Dictionary<string, object>() { { "Test0", "Test0" } });
        }
    }
}
