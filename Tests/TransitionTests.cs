using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;

namespace Tests
{
    //[TestClass]
    //public class TransitionTests
    //{
    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void AllTestsWithLogger()
    //    {
    //        ForTest.stateMachine = new StateMachine(ForTest.GetConsoleLogger());
    //        AllTests();
    //    }
    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void AllTestsWithoutLogger()
    //    {
    //        AllTests();
    //    }
    //    public void AllTests()
    //    {
    //        AddTransitionTest();
    //        GetTransitionTest();
    //        DeleteTransitionTest();
    //        InvokeTransitionTest();
    //        TransitionParametersTest();
    //    }
    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void AddTransitionTest()
    //    {
    //        StateMachine stateMachine = ForTest.stateMachine;
    //        string transitionName = "Transition1";
    //        string stateName1 = "State1";
    //        string stateName2 = "State2";
    //        bool result;          
    //        State state1 = stateMachine.AddState(stateName1);
    //        State state2 = stateMachine.AddState(stateName2);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName));

    //        //add transition from two objects
    //        Transition transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition from name and object
    //        transition1 = stateMachine.AddTransition(transitionName, stateName1, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = stateMachine.AddTransition(transitionName, stateName1, state2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition from object and name
    //        transition1 = stateMachine.AddTransition(transitionName, state1, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = stateMachine.AddTransition(transitionName, state1, stateName2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition from two names
    //        transition1 = stateMachine.AddTransition(transitionName, stateName1, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = stateMachine.AddTransition(transitionName, stateName1, stateName2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from two objects
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, state1, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, state1, state2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from name and object
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, stateName1, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, stateName1, state2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from object and name
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, state1, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, state1, stateName2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from two names
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, stateName1, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = stateMachine.TryAddTransition(out result, transitionName, stateName1, stateName2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition from this state and object
    //        transition1 = state1.AddTransitionFromThis(transitionName, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = state1.AddTransitionFromThis(transitionName, state2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition from this state and name
    //        transition1 = state1.AddTransitionFromThis(transitionName, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = state1.AddTransitionFromThis(transitionName, stateName2);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from this state and object
    //        transition1 = state1.TryAddTransitionFromThis(out result, transitionName, state2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = state1.TryAddTransitionFromThis(out result, transitionName, state2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition from this state and name
    //        transition1 = state1.TryAddTransitionFromThis(out result, transitionName, stateName2);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = state1.TryAddTransitionFromThis(out result, transitionName, stateName2);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition to this state and object
    //        transition1 = state2.AddTransitionToThis(transitionName, state1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = state1.AddTransitionFromThis(transitionName, state1);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //add transition to this state and name
    //        transition1 = state2.AddTransitionToThis(transitionName, stateName1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<ArgumentException>(() =>
    //        {
    //            transition1 = state1.AddTransitionFromThis(transitionName, stateName1);
    //        });
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition to this state and object
    //        transition1 = state2.TryAddTransitionToThis(out result, transitionName, state1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = state2.TryAddTransitionToThis(out result, transitionName, state1);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        //try add transition to this state and name
    //        transition1 = state2.TryAddTransitionToThis(out result, transitionName, stateName1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        transition1 = state2.TryAddTransitionToThis(out result, transitionName, stateName1);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName));
    //        stateMachine.DeleteTransition(transitionName);

    //        stateMachine.DeleteState(stateName1);
    //        stateMachine.DeleteState(stateName2);

    //    }

    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void GetTransitionTest()
    //    {
    //        StateMachine stateMachine = ForTest.stateMachine;
    //        string transitionName1 = "Transition1";
    //        string transitionName2 = "Transition2";
    //        string stateName1 = "State1";
    //        string stateName2 = "State2";
    //        bool result;
    //        Transition transition1;
    //        Transition transition2;
    //        Dictionary<string, Transition> transitions;
    //        State state1 = stateMachine.AddState(stateName1);
    //        State state2 = stateMachine.AddState(stateName2);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName1));

    //        //get transition
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition1 = stateMachine.GetTransition(transitionName1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName1));
    //        stateMachine.DeleteTransition(transitionName1);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transition1 = stateMachine.GetTransition(transitionName1);
    //        });

    //        //try get transition
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition1 = stateMachine.TryGetTransition(transitionName1, out result);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsTrue(stateMachine.TransitionExists(transitionName1));
    //        stateMachine.DeleteTransition(transitionName1);
    //        transition1 = stateMachine.TryGetTransition(transitionName1, out result);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);


    //        //get all transitions from state by object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.GetTransitionsFromState(state1);
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = stateMachine.GetTransitionsFromState(state1);
    //        });

    //        //get all transitions from state by name
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.GetTransitionsFromState(stateName1);
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = stateMachine.GetTransitionsFromState(stateName1);
    //        });

    //        //try get all transitions from state by object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.TryGetTransitionsFromState(state1, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = stateMachine.TryGetTransitionsFromState(state1, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        //try get all transitions from state by name
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.TryGetTransitionsFromState(stateName1, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = stateMachine.TryGetTransitionsFromState(stateName1, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        //get all transitions from state from object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = state1.GetTransitionsFromThis();
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = state1.GetTransitionsFromThis();
    //        });

    //        //try get all transitions from state from object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = state1.TryGetTransitionsFromThis(out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = state1.TryGetTransitionsFromThis(out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        //get all transitions to state by object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.GetTransitionsToState(state2);
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = stateMachine.GetTransitionsToState(state2);
    //        });

    //        //get all transitions to state by name
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.GetTransitionsToState(stateName2);
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = stateMachine.GetTransitionsToState(stateName2);
    //        });

    //        //try get all transitions to state by object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.TryGetTransitionsToState(state2, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = stateMachine.TryGetTransitionsToState(state2, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        //try get all transitions to state by name
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = stateMachine.TryGetTransitionsToState(stateName2, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = stateMachine.TryGetTransitionsToState(stateName2, out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        //get all transitions to state from object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = state2.GetTransitionsToThis();
    //        Assert.IsNotNull(transitions);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transitions = state2.GetTransitionsToThis();
    //        });

    //        //try get all transitions to state from object
    //        transition1 = stateMachine.AddTransition(transitionName1, state1, state2);
    //        transition2 = stateMachine.AddTransition(transitionName2, state1, state2);
    //        transitions = state2.TryGetTransitionsToThis(out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsTrue(result);
    //        Assert.AreEqual(transitions.Count, 2);
    //        stateMachine.DeleteTransition(transitionName1);
    //        stateMachine.DeleteTransition(transitionName2);
    //        transitions = state2.TryGetTransitionsToThis(out result);
    //        Assert.IsNotNull(transitions);
    //        Assert.IsFalse(result);
    //        Assert.AreEqual(transitions.Count, 0);

    //        stateMachine.DeleteState(stateName1);
    //        stateMachine.DeleteState(stateName2);

    //    }

    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void DeleteTransitionTest()
    //    {
    //        StateMachine stateMachine = ForTest.stateMachine;
    //        string transitionName = "Transition1";
    //        string stateName1 = "State1";
    //        string stateName2 = "State2";
    //        bool result;
    //        Transition transition1;
    //        State state1 = stateMachine.AddState(stateName1);
    //        State state2 = stateMachine.AddState(stateName2);

    //        //delete transition by name
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = stateMachine.DeleteTransition(transitionName);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transition1 = stateMachine.DeleteTransition(transitionName);
    //        });

    //        //delete transition by object
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = stateMachine.DeleteTransition(transition1);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transition1 = stateMachine.DeleteTransition(transition1);
    //        });

    //        //try delete transition by name
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = stateMachine.TryDeleteTransition(transitionName, out result);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        transition1 = stateMachine.TryDeleteTransition(transitionName, out result);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);

    //        //try delete transition by object
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = stateMachine.TryDeleteTransition(transition1, out result);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        transition1 = stateMachine.TryDeleteTransition(transition1, out result);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);

    //        //delete transition from object
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = transition1.Delete();
    //        Assert.IsNotNull(transition1);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName));
    //        Assert.ThrowsException<KeyNotFoundException>(() =>
    //        {
    //            transition1 = transition1.Delete();
    //        });

    //        //try delete transition from object
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1 = transition1.TryDelete(out result);
    //        Assert.IsNotNull(transition1);
    //        Assert.IsTrue(result);
    //        Assert.IsFalse(stateMachine.TransitionExists(transitionName));
    //        transition1 = transition1.TryDelete(out result);
    //        Assert.IsNull(transition1);
    //        Assert.IsFalse(result);

    //        stateMachine.DeleteState(stateName1);
    //        stateMachine.DeleteState(stateName2);

    //    }

    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void InvokeTransitionTest()
    //    {
    //        StateMachine stateMachine = ForTest.stateMachine;
    //        int eventCount = 0;

    //        string transitionName = "Transition1";
    //        string stateName1 = "State1";
    //        string stateName2 = "State2";
    //        Transition transition1;
    //        State state1 = stateMachine.AddState(stateName1, MethodOnEntry);
    //        State state2 = stateMachine.AddState(stateName2);


    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        state1.SetAsStartState();
    //        stateMachine.Start();
    //        Assert.AreEqual(eventCount, 0);
    //        stateMachine.DeleteTransition(transitionName);


    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2, MethodOnInvoke);
    //        state1.SetAsStartState();
    //        stateMachine.Start();        
    //        Assert.AreEqual(eventCount, 1);
    //        stateMachine.DeleteTransition(transitionName);

    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2).OnInvoke(MethodOnInvoke);
    //        state1.SetAsStartState();
    //        stateMachine.Start();
    //        Assert.AreEqual(eventCount, 1);
    //        stateMachine.DeleteTransition(transitionName);

    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2);
    //        transition1.OnInvoke(MethodOnInvoke);
    //        state1.SetAsStartState();
    //        stateMachine.Start();
    //        Assert.AreEqual(eventCount, 1);
    //        stateMachine.DeleteTransition(transitionName);


    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2, MethodOnInvoke).OnInvoke(MethodOnInvoke).OnInvoke(MethodOnInvoke);
    //        transition1.OnInvoke(MethodOnInvoke);
    //        transition1.OnInvoke(MethodOnInvoke);
    //        state1.SetAsStartState();
    //        stateMachine.Start();
    //        Assert.AreEqual(eventCount, 5);
    //        stateMachine.DeleteTransition(transitionName);

    //        stateMachine.DeleteState(stateName1);
    //        stateMachine.DeleteState(stateName2);

    //        void MethodOnInvoke(Transition transition, Dictionary<string, object> parameters)
    //        {
    //            Assert.AreEqual(transition.Name, transitionName);
    //            eventCount++;
    //        }
    //        void MethodOnEntry(State state, Dictionary<string, object> parameters)
    //        {
    //           state.StateMachine.InvokeTransition(transitionName);
    //        }
    //    }

    //    [TestMethod]
    //    [TestCategory("Transition")]
    //    public void TransitionParametersTest()
    //    {
    //        StateMachine stateMachine = ForTest.stateMachine;
    //        int eventCount = 0;
    //        string transitionName = "Transition1";
    //        string stateName1 = "State1";
    //        string stateName2 = "State2";
    //        string intName = "Int";
    //        string doubleName = "Double";
    //        string stringName = "String";
    //        string boolName = "Bool";
    //        Dictionary<string, object> parameters1 = new Dictionary<string, object>() { { intName, 15 }};
    //        Dictionary<string, object> parameters2 = new Dictionary<string, object>() { { doubleName, 15.5 } };
    //        Dictionary<string, object> parameters3 = new Dictionary<string, object>() { { stringName, "string" } };
    //        Transition transition1;
    //        State state1 = stateMachine.AddState(stateName1, MethodOnEntry1);
    //        State state2 = stateMachine.AddState(stateName2, MethodOnEntry2);

    //        eventCount = 0;
    //        transition1 = stateMachine.AddTransition(transitionName, state1, state2, MethodOnInvoke);
    //        state1.SetAsStartState();
    //        stateMachine.Start(parameters1);
    //        Assert.AreEqual(eventCount, 1);
    //        stateMachine.DeleteTransition(transitionName);

    //        stateMachine.DeleteState(stateName1);
    //        stateMachine.DeleteState(stateName2);

    //        void MethodOnInvoke(Transition transition, Dictionary<string, object> parameters)
    //        {
    //            Assert.AreEqual(transition.Name, transitionName);
    //            eventCount++;
    //        }
    //        void MethodOnEntry1(State state, Dictionary<string, object> parameters)
    //        {
    //            Assert.AreEqual(state.Name, stateName1);
    //            Assert.IsTrue(parameters.ContainsKey(intName));
    //            Assert.AreEqual(parameters.Count, 1);
    //            state.StateMachine.InvokeTransition(transitionName, parameters2).AddParameter(boolName, true).AddParameters(parameters3);
    //        }
    //        void MethodOnEntry2(State state, Dictionary<string, object> parameters)
    //        {
    //            Assert.IsFalse(parameters.ContainsKey(intName));
    //            Assert.IsTrue(parameters.ContainsKey(doubleName));
    //            Assert.IsTrue(parameters.ContainsKey(stringName));
    //            Assert.IsTrue(parameters.ContainsKey(boolName));
    //            Assert.AreEqual(parameters.Count, 3);
    //            Assert.AreEqual(state.Name, stateName2);
    //        }


    //    }

    //}
}
