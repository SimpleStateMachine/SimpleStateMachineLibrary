using System;
using System.Collections.Generic;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        public Dictionary<string, Transition> GetTransitionsToThis()
        {
            return this.StateMachine.GetTransitionsToState(this);
        }

        public Dictionary<string, Transition> TryGetTransitionsToThis(out bool result)
        {
            return this.StateMachine.TryGetTransitionsToState(this, out result);
        }

        public Transition AddTransitionToThis(string nameTransition, State stateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.AddTransition(nameTransition, stateFrom, this);
        }

        public Transition AddTransitionToThis(string nameTransition, string nameStateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.AddTransition(nameTransition, nameStateFrom, this);
        }

        public Transition TryAddTransitionToThis(out bool result, string nameTransition, State stateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.TryAddTransition(out result, nameTransition, stateFrom, this, actionOnInvoke);
        }

        public Transition TryAddTransitionToThis(out bool result, string nameTransition, string nameStateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return this.StateMachine.TryAddTransition(out result, nameTransition, nameStateFrom, this, actionOnInvoke);
        }
    }
}
