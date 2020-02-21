using System.Collections.Generic;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {

        public List<Transition> GetTransitionsToThis()
        {
            return this.StateMachine.GetTransitionsToState(this);
        }

        public List<Transition> TryGetTransitionsToThis()
        {
            return this.StateMachine.TryGetTransitionsToState(this);
        }

        public Transition AddTransitionToThis(string nameTrancition, State stateFrom)
        {
            return this.StateMachine.AddTransition(nameTrancition, stateFrom, this);
        }

        public Transition AddTransitionToThis(string nameTrancition, string nameStateFrom)
        {
            return this.StateMachine.AddTransition(nameTrancition, nameStateFrom, this);
        }

        public Transition TryAddTransitionToThis(string nameTrancition, State stateFrom)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, stateFrom, this);
        }

        public Transition TryAddTransitionToThis(string nameTrancition, string nameStateFrom)
        {
            return this.StateMachine.TryAddTransition(nameTrancition, nameStateFrom, this);
        }

    }
}
