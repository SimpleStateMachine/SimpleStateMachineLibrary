
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        public Dictionary<string, Transition> GetTransitionsFromThis()
        {
            return this.StateMachine.GetTransitionsFromState(this);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromThis()
        {
            return this.StateMachine.TryGetTransitionsFromState(this);
        }

        public Transition AddTransitionFromThis(string nameTransition, State stateTo)
        {
            return this.StateMachine.AddTransition(nameTransition, this, stateTo);
        }

        public Transition AddTransitionFromThis(string nameTransition, string nameStateTo)
        {
            return this.StateMachine.AddTransition(nameTransition, this, nameStateTo);
        }

        public Transition TryAddTransitionFromThis(string nameTransition, State stateTo, out bool result)
        {
            return this.StateMachine.TryAddTransition(nameTransition, this, stateTo, out result);
        }

        public Transition TryAddTransitionFromThis(string nameTransition, string nameStateTo, out bool result)
        {
            return this.StateMachine.TryAddTransition(nameTransition, this, nameStateTo, out result);
        }
    }
}
