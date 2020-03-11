using System.Collections.Generic;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {

        public Dictionary<string, Transition> GetTransitionsToThis()
        {
            return this.StateMachine.GetTransitionsToState(this);
        }

        public Dictionary<string, Transition> TryGetTransitionsToThis()
        {
            return this.StateMachine.TryGetTransitionsToState(this);
        }

        public Transition AddTransitionToThis(string nameTransition, State stateFrom)
        {
            return this.StateMachine.AddTransition(nameTransition, stateFrom, this);
        }

        public Transition AddTransitionToThis(string nameTransition, string nameStateFrom)
        {
            return this.StateMachine.AddTransition(nameTransition, nameStateFrom, this);
        }

        public Transition TryAddTransitionToThis(string nameTransition, State stateFrom, out bool result)
        {
            return this.StateMachine.TryAddTransition(nameTransition, stateFrom, this, out result);
        }

        public Transition TryAddTransitionToThis(string nameTransition, string nameStateFrom, out bool result)
        {
            return this.StateMachine.TryAddTransition(nameTransition, nameStateFrom, this, out result);
        }

    }
}
