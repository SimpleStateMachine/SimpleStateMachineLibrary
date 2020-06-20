using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        internal Dictionary<string, Transition> _GetTransitionsFromState(string stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(_states, stateName, this._logger, exceptions);
            var transitionsFromState = result ? Check.GetValuesWhere(_transitions, (Transition x) => x.StateFrom == stateName, this._logger, out result, exceptions): new Dictionary<string, Transition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", stateName);

            return transitionsFromState;
        }

        internal Dictionary<string, Transition> _GetTransitionsFromState(State state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(_states, state, this._logger, exceptions);
            var transitionsFromState = result ? Check.GetValuesWhere(_transitions, (Transition x) => x.StateFrom == state.Name, this._logger, out result, exceptions) : new Dictionary<string, Transition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", state.Name);

            return transitionsFromState;
        }

        public Dictionary<string, Transition> GetTransitionsFromState(string stateName)
        {
            return _GetTransitionsFromState(stateName, out bool result,  true, true);
        }

        public Dictionary<string, Transition> GetTransitionsFromState(State state)
        {
            return _GetTransitionsFromState(state, out bool result, true, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(string stateName, out bool result)
        {
            return _GetTransitionsFromState(stateName, out result, false, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsFromState(State state, out bool result)
        {
            return _GetTransitionsFromState(state, out result,  false, true);
        }



        internal Dictionary<string, Transition> _GetTransitionsToState(string stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(_states, stateName, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere(_transitions, (Transition x) => x.StateTo == stateName, this._logger, out result, exceptions): new Dictionary<string, Transition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", stateName);

            return transitionsToState;
        }

        internal Dictionary<string, Transition> _GetTransitionsToState(State state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(_states, state, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere(_transitions, (Transition x) => x.StateTo == state.Name, this._logger, out result, exceptions) : new Dictionary<string, Transition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", state.Name);

            return transitionsToState;
        }

        public Dictionary<string, Transition> GetTransitionsToState(string stateName)
        {
            return _GetTransitionsToState(stateName, out bool result, true, true);
        }

        public Dictionary<string, Transition> GetTransitionsToState(State state)
        {
            return _GetTransitionsToState(state, out bool result, true, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(string stateName, out bool result)
        {
            return _GetTransitionsToState(stateName, out result, false, true);
        }

        public Dictionary<string, Transition> TryGetTransitionsToState(State state, out bool result)
        {
            return _GetTransitionsToState(state, out result, false, true);
        }

    }
}
