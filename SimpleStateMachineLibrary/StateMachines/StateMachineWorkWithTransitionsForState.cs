using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        public Dictionary<string, ITransition> GetTransitionsFromState(string stateName)
        {
            return _GetTransitionsFromState(stateName, out var result,  true, true);
        }
        public Dictionary<string, ITransition> GetTransitionsFromState(IState state)
        {
            return _GetTransitionsFromState(state, out var result, true, true);
        }
        public Dictionary<string, ITransition> TryGetTransitionsFromState(string stateName, out bool result)
        {
            return _GetTransitionsFromState(stateName, out result, false, true);
        }
        public Dictionary<string, ITransition> TryGetTransitionsFromState(IState state, out bool result)
        {
            return _GetTransitionsFromState(state, out result,  false, true);
        }
        public Dictionary<string, ITransition> GetTransitionsToState(string stateName)
        {
            return _GetTransitionsToState(stateName, out var result, true, true);
        }
        public Dictionary<string, ITransition> GetTransitionsToState(IState state)
        {
            return _GetTransitionsToState(state, out var result, true, true);
        }
        public Dictionary<string, ITransition> TryGetTransitionsToState(string stateName, out bool result)
        {
            return _GetTransitionsToState(stateName, out result, false, true);
        }
        public Dictionary<string, ITransition> TryGetTransitionsToState(IState state, out bool result)
        {
            return _GetTransitionsToState(state, out result, false, true);
        }
        
        
        internal Dictionary<string, ITransition> _GetTransitionsFromState(string stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(States, stateName, this._logger, exceptions);
            var transitionsFromState = result ? Check.GetValuesWhere(Transitions, (ITransition x) => x.StateFrom.Name == stateName, this._logger, out result, exceptions): new Dictionary<string, ITransition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", stateName);

            return transitionsFromState;
        }
        internal Dictionary<string, ITransition> _GetTransitionsFromState(IState state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(States, state, this._logger, exceptions);
            var transitionsFromState = result ? Check.GetValuesWhere(Transitions, (ITransition x) => x.StateFrom == state.Name, this._logger, out result, exceptions) : new Dictionary<string, ITransition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", state.Name);

            return transitionsFromState;
        }
        
        internal Dictionary<string, ITransition> _GetTransitionsToState(string stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(States, stateName, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere(Transitions, (ITransition x) => x.StateTo == stateName, this._logger, out result, exceptions): new Dictionary<string, ITransition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", stateName);

            return transitionsToState;
        }
        internal Dictionary<string, ITransition> _GetTransitionsToState(IState state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains(States, state, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere(Transitions, (ITransition x) => x.StateTo == state.Name, this._logger, out result, exceptions) : new Dictionary<string, ITransition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", state.Name);

            return transitionsToState;
        }
        
    }
}
