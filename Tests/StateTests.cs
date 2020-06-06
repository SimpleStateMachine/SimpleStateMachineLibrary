using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Tests
{
    [TestClass]
    public class StateTests
    {
        [TestMethod]
        [TestCategory("State")]
        public void AllTestsWithLogger()
        {
            ForTest.stateMachine = new StateMachine(ForTest.GetConsoleLogger());
            AllTests();
        }
        [TestMethod]
        [TestCategory("State")]
        public void AllTestsWithoutLogger()
        {
            ForTest.stateMachine = new StateMachine();
            AllTests();
        }
        public void AllTests()
        {
            AddStateTest();
            GetStateTest();
            DeleteStateTest();
            EntryExitStateTest();
        }
 
        [TestMethod]
        [TestCategory("State")]
        public void AddStateTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string stateName = "State1";
            Assert.IsFalse(stateMachine.StateExists(stateName));

            State state1 = stateMachine.AddState(stateName);
            Assert.IsNotNull(state1);

            Assert.IsTrue(stateMachine.StateExists(stateName));

            Assert.ThrowsException<ArgumentException>(() =>
            {
                state1 = stateMachine.AddState(stateName);
            });

            state1 = stateMachine.TryAddState(out bool result, stateName);
            Assert.IsNull(state1);
            Assert.IsFalse(result);

            stateMachine.DeleteState(stateName);

        }

        [TestMethod]
        [TestCategory("State")]
        public void GetStateTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string stateName = "State1";
            State state1;
            bool result;

            Assert.IsFalse(stateMachine.StateExists(stateName));

            //get state
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.GetState(stateName);
            Assert.IsNotNull(state1);
            Assert.IsTrue(stateMachine.StateExists(stateName));
            stateMachine.DeleteState(stateName);
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                state1 = stateMachine.GetState(stateName);
            });

            //try get state
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.TryGetState(stateName, out result);
            Assert.IsNotNull(state1);
            Assert.IsTrue(result);
            Assert.IsTrue(stateMachine.StateExists(stateName));
            stateMachine.DeleteState(stateName);
            state1 = stateMachine.TryGetState(stateName, out result);
            Assert.IsNull(state1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("State")]
        public void DeleteStateTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string stateName = "State1";
            State state1;
            bool result;

            //delete state by name
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.DeleteState(stateName);
            Assert.IsNotNull(state1);
            Assert.IsFalse(stateMachine.StateExists(stateName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                state1 = stateMachine.DeleteState(stateName);
            });

            //delete state by object
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.DeleteState(state1);
            Assert.IsNotNull(state1);
            Assert.IsFalse(stateMachine.StateExists(stateName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                state1 = stateMachine.DeleteState(state1);
            });

            //try delete state by name
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.TryDeleteState(stateName, out result);
            Assert.IsNotNull(state1);
            Assert.IsTrue(result);
            state1 = stateMachine.TryDeleteState(stateName, out result);
            Assert.IsNull(state1);
            Assert.IsFalse(result);

            //try delete state by object
            state1 = stateMachine.AddState(stateName);
            state1 = stateMachine.TryDeleteState(state1, out result);
            Assert.IsNotNull(state1);
            Assert.IsTrue(result);
            state1 = stateMachine.TryDeleteState(state1, out result);
            Assert.IsNull(state1);
            Assert.IsFalse(result);

            //delete state from object
            state1 = stateMachine.AddState(stateName);
            state1 = state1.Delete();
            Assert.IsNotNull(state1);
            Assert.IsFalse(stateMachine.StateExists(stateName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                state1 = state1.Delete();
            });

            //try delete state from object
            state1 = stateMachine.AddState(stateName);
            state1 = state1.TryDelete(out result);
            Assert.IsNotNull(state1);
            Assert.IsTrue(result);
            Assert.IsFalse(stateMachine.StateExists(stateName));
            state1 = state1.TryDelete(out result);
            Assert.IsNull(state1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("State")]
        public void EntryExitStateTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            int eventCont;
            string stateName1 = "State1";
            State state1;

            state1 = stateMachine.AddState(stateName1);
            eventCont = 0;
            state1.SetAsStartState();
            stateMachine.Start();
            Assert.AreEqual(eventCont, 0);
            stateMachine.DeleteState(stateName1);


            state1 = stateMachine.AddState(stateName1);
            eventCont = 0;
            state1.OnEntry(MethodOnEntry);
            state1.OnExit(MethodOnExit);
            state1.SetAsStartState();
            stateMachine.Start();
            Assert.AreEqual(eventCont, 2);
            stateMachine.DeleteState(stateName1);

            state1 = stateMachine.AddState(stateName1, MethodOnEntry, MethodOnExit);
            eventCont = 0;
            state1.SetAsStartState();
            stateMachine.Start();
            Assert.AreEqual(eventCont, 2);
            stateMachine.DeleteState(stateName1);

            state1 = stateMachine.AddState(stateName1).OnEntry(MethodOnEntry).OnExit(MethodOnExit);
            eventCont = 0;
            state1.SetAsStartState();
            stateMachine.Start();
            Assert.AreEqual(eventCont, 2);
            stateMachine.DeleteState(stateName1);

            state1 = stateMachine.AddState(stateName1, MethodOnEntry, MethodOnExit).OnEntry(MethodOnEntry).OnExit(MethodOnExit).OnEntry(MethodOnEntry).OnExit(MethodOnExit);
            eventCont = 0;
            state1.OnEntry(MethodOnEntry);
            state1.OnEntry(MethodOnEntry);
            state1.OnExit(MethodOnExit);
            state1.OnExit(MethodOnExit);
            state1.SetAsStartState();
            stateMachine.Start();
            Assert.AreEqual(eventCont, 10);
            stateMachine.DeleteState(stateName1);


            void MethodOnEntry(State state, Dictionary<string, object> parameters)
            {
                Assert.AreEqual(state.Name, stateName1);
                eventCont++;
            }
            void MethodOnExit(State state, Dictionary<string, object> parameters)
            {
                Assert.AreEqual(state.Name, stateName1);
                eventCont++;
            }
        }

    }
}

