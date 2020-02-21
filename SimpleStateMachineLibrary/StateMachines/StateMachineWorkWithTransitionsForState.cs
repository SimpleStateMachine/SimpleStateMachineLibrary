using SimpleStateMachineLibrary.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        private List<Transition> GetTransitionsFromState(string stateName, bool exeptions)
        {
            bool contains = Check.Contains(_states, stateName, exeptions);
            return contains ? _transitions.Values.Where(x => x.StateFrom.Name == stateName).ToList() : new List<Transition>();
        }

        private List<Transition> GetTransitionsFromState(State state, bool exeptions)
        {
            bool contains = Check.Contains(_states, state, exeptions);
            return contains ? _transitions.Values.Where(x => x.StateFrom.Name == state.Name).ToList() : new List<Transition>();
        }

        private List<Transition> GetTransitionsToState(string stateName, bool exeptions)
        {
            bool contains = Check.Contains(_states, stateName, exeptions);
            return contains ? _transitions.Values.Where(x => x.StateTo.Name == stateName).ToList() : new List<Transition>();
        }

        private List<Transition> GetTransitionsToState(State state, bool exeptions)
        {
            bool contains = Check.Contains(_states, state, exeptions);
            return contains ? _transitions.Values.Where(x => x.StateTo.Name == state.Name).ToList() : new List<Transition>();
        }

        public List<Transition> GetTransitionsFromState(string stateName)
        {
            return GetTransitionsFromState(stateName, true);
        }

        public List<Transition> GetTransitionsFromState(State state)
        {
            return GetTransitionsFromState(state, true);
        }

        public List<Transition> TryGetTransitionsFromState(string stateName)
        {
            return GetTransitionsFromState(stateName, false);
        }

        public List<Transition> TryGetTransitionsFromState(State state)
        {
            return GetTransitionsFromState(state, false);
        }

        public List<Transition> GetTransitionsToState(string stateName)
        {
            return GetTransitionsToState(stateName, true);
        }

        public List<Transition> GetTransitionsToState(State state)
        {
            return GetTransitionsToState(state, true);
        }

        public List<Transition> TryGetTransitionsToState(string stateName)
        {
            return GetTransitionsToState(stateName, false);
        }

        public List<Transition> TryGetTransitionsToState(State state)
        {
            return GetTransitionsToState(state, false);
        }

    }
}
