
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

        public Dictionary<string, Transition> TryGetTransitionsFromThis(out bool result)
        {
            return this.StateMachine.TryGetTransitionsFromState(this, out result);
        }

        public Transition AddTransitionFromThis(string nameTransition, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.AddTransition(nameTransition, this, stateTo, actionOnInvoke);
        }

        public Transition AddTransitionFromThis(string nameTransition, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.AddTransition(nameTransition, this, nameStateTo, actionOnInvoke);
        }

        public Transition TryAddTransitionFromThis(out bool result, string nameTransition, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.TryAddTransition(out result, nameTransition, this, stateTo, actionOnInvoke);
        }

        public Transition TryAddTransitionFromThis(out bool result, string nameTransition, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.TryAddTransition(out result, nameTransition, this, nameStateTo, actionOnInvoke);
        }
    }
}
