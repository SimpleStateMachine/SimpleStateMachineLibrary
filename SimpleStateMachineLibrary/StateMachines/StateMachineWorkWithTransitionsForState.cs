using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        private Dictionary<string, Transition> GetTransitionsFromState(string stateName, out bool result, bool exceptions)
        {
            result = Check.Contains(_states, stateName, this._logger, exceptions);
            var transitionsFromState = result ? _transitions.Values.Where(x => x.StateFrom.Name == stateName).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions from state \"{NameState}\" ", stateName);
            return transitionsFromState;
        }

        private Dictionary<string, Transition> GetTransitionsFromState(State state, out bool result, bool exceptions)
        {
            result = Check.Contains(_states, state, this._logger, exceptions);
            var transitionsFromState = result ? _transitions.Values.Where(x => x.StateFrom.Name == state.Name).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions from state \"{NameState}\" ", state.Name);
            return transitionsFromState;
        }

        public Dictionary<string, Transition> GetTransitionsFromState(string stateName)
        {
            return GetTransitionsFromState(stateName, out bool result,  true);
        }

        public Dictionary<string, Transition> GetTransitionsFromState(State state)
        {
            return GetTransitionsFromState(state, out bool result, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(string stateName, out bool result)
        {
            return GetTransitionsFromState(stateName, out result, false);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(State state, out bool result)
        {
            return GetTransitionsFromState(state, out result,  false);
        }



        private Dictionary<string, Transition> GetTransitionsToState(string stateName, out bool result, bool exceptions)
        {
            result = Check.Contains(_states, stateName, this._logger, exceptions);
            var transitionsToState = result ? _transitions.Values.Where(x => x.StateTo.Name == stateName).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions to state \"{NameState}\" ", stateName);
            return transitionsToState;
        }

        private Dictionary<string, Transition> GetTransitionsToState(State state, out bool result, bool exceptions)
        {
            result = Check.Contains(_states, state, this._logger, exceptions);
            var transitionsToState = result ? _transitions.Values.Where(x => x.StateTo.Name == state.Name).ToDictionary(x => x.Name, x => x) : new Dictionary<string, Transition>();

            _logger?.LogDebug("Get transitions to state \"{NameState}\" ", state.Name);
            return transitionsToState;
        }

        public Dictionary<string, Transition> GetTransitionsToState(string stateName)
        {
            return GetTransitionsToState(stateName, out bool result, true);
        }

        public Dictionary<string, Transition> GetTransitionsToState(State state)
        {
            return GetTransitionsToState(state, out bool result, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(string stateName, out bool result)
        {
            return GetTransitionsToState(stateName, out result, false);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(State state, out bool result)
        {
            return GetTransitionsToState(state, out result, false);
        }

    }
}
