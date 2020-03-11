using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        private Dictionary<string, Transition> GetTransitionsFromState(string stateName, bool exeptions)
        {
            bool contains = Check.Contains(_states, stateName, exeptions);
            var transitionsFromState = contains ? _transitions.Values.Where(x => x.StateFrom.Name == stateName).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions from state \"{NameState}\" ", stateName);
            return transitionsFromState;
        }

        private Dictionary<string, Transition> GetTransitionsFromState(State state, bool exeptions)
        {
            bool contains = Check.Contains(_states, state, exeptions);
            var transitionsFromState = contains ? _transitions.Values.Where(x => x.StateFrom.Name == state.Name).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions from state \"{NameState}\" ", state.Name);
            return transitionsFromState;
        }

        private Dictionary<string, Transition> GetTransitionsToState(string stateName, bool exeptions)
        {
            bool contains = Check.Contains(_states, stateName, exeptions);
            var transitionsToState = contains ? _transitions.Values.Where(x => x.StateTo.Name == stateName).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions to state \"{NameState}\" ", stateName);
            return transitionsToState;
        }

        private Dictionary<string, Transition> GetTransitionsToState(State state, bool exeptions)
        {
            bool contains = Check.Contains(_states, state, exeptions);
            var transitionsToState = contains ? _transitions.Values.Where(x => x.StateTo.Name == state.Name).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions to state \"{NameState}\" ", state.Name);
            return transitionsToState;
        }

        public Dictionary<string, Transition> GetTransitionsFromState(string stateName)
        {
            return GetTransitionsFromState(stateName, true);
        }

        public Dictionary<string, Transition> GetTransitionsFromState(State state)
        {
            return GetTransitionsFromState(state, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(string stateName)
        {
            return GetTransitionsFromState(stateName, false);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(State state)
        {
            return GetTransitionsFromState(state, false);
        }

        public Dictionary<string, Transition> GetTransitionsToState(string stateName)
        {
            return GetTransitionsToState(stateName, true);
        }

        public Dictionary<string, Transition> GetTransitionsToState(State state)
        {
            return GetTransitionsToState(state, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(string stateName)
        {
            return GetTransitionsToState(stateName, false);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(State state)
        {
            return GetTransitionsToState(state, false);
        }

    }
}
