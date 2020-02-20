
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        public List<Transition> GetTransitionsFromThis()
        {
            return this.StateMachine.GetTransitionsFromState(this);
        }

        public Transition AddTransitionFromThis(string nameTrancition, State stateTo)
        {
            return this.StateMachine.AddTransition(nameTrancition, this, StateMachine.State(stateTo));
        }

        public Transition AddTransitionFromThis(string nameTrancition, string nameStateTo)
        {
            return this.StateMachine.AddTransition(nameTrancition, this, StateMachine.State(nameStateTo));
        }

        public Transition TryAddTransitionFromThis(string nameTrancition, State stateTo)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, this, stateTo);
        }

        public Transition TryAddTransitionFromThis(string nameTrancition, string nameStateTo)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, this, nameStateTo);
        }
    }
}
