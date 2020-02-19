using System.Collections.Generic;

using SimpleStateMachineLibrary.Helpers;
using SimpleStateMachineLibrary.Transitions;

namespace SimpleStateMachineLibrary.States
{
    public partial class State : NamedObject
    {

        public List<Transition> GetTransitionsToThis()
        {
            return this.StateMachine.GetTransitionsToState(this);
        }

        public Transition AddTransitionToThis(string nameTrancition, State stateFrom)
        {
            return this.StateMachine.AddTransition(nameTrancition, StateMachine.State(stateFrom), this);
        }

        public Transition AddTransitionToThis(string nameTrancition, string nameStateFrom)
        {
            return this.StateMachine.AddTransition(nameTrancition, StateMachine.State(nameStateFrom), this);
        }

        public Transition TryAddTransitionToThis(string nameTrancition, State stateFrom)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, StateMachine.State(stateFrom), this);
        }

        public Transition TryAddTransitionToThis(string nameTrancition, string nameStateFrom)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, StateMachine.State(nameStateFrom), this);
        }

    }
}
