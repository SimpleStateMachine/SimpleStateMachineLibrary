using System;
using System.Collections.Generic;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {
        public partial class State
        {
            public Dictionary<TKeyTransition, Transition> GetTransitionsToThis()
            {
                return this.StateMachine.GetTransitionsToState(this);
            }

            public Dictionary<TKeyTransition, Transition> TryGetTransitionsToThis(out bool result)
            {
                return this.StateMachine.TryGetTransitionsToState(this, out result);
            }

            public Transition AddTransitionToThis(TKeyTransition nameTransition, State stateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.AddTransition(nameTransition, stateFrom, this);
            }

            public Transition AddTransitionToThis(TKeyTransition nameTransition, TKeyState nameStateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.AddTransition(nameTransition, nameStateFrom, this);
            }

            public Transition TryAddTransitionToThis(out bool result, TKeyTransition nameTransition, State stateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.TryAddTransition(out result, nameTransition, stateFrom, this, actionOnInvoke);
            }

            public Transition TryAddTransitionToThis(out bool result, TKeyTransition nameTransition, TKeyState nameStateFrom, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.TryAddTransition(out result, nameTransition, nameStateFrom, this, actionOnInvoke);
            }
        }
    }
}
