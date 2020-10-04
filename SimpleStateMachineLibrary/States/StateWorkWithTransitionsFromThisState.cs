
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {
        public partial class State
        {
            public Dictionary<TKeyTransition, Transition> GetTransitionsFromThis()
            {
                return this.StateMachine.GetTransitionsFromState(this);
            }

            public Dictionary<TKeyTransition, Transition> TryGetTransitionsFromThis(out bool result)
            {
                return this.StateMachine.TryGetTransitionsFromState(this, out result);
            }

            public Transition AddTransitionFromThis(TKeyTransition nameTransition, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.AddTransition(nameTransition, this, stateTo, actionOnInvoke);
            }

            public Transition AddTransitionFromThis(TKeyTransition nameTransition, TKeyState nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.AddTransition(nameTransition, this, nameStateTo, actionOnInvoke);
            }

            public Transition TryAddTransitionFromThis(out bool result, TKeyTransition nameTransition, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.TryAddTransition(out result, nameTransition, this, stateTo, actionOnInvoke);
            }

            public Transition TryAddTransitionFromThis(out bool result, TKeyTransition nameTransition, TKeyState nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
            {
                return this.StateMachine.TryAddTransition(out result, nameTransition, this, nameStateTo, actionOnInvoke);
            }
        }
    }
}
