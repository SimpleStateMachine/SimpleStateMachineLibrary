using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {
        internal Dictionary<TKeyTransition, Transition> _GetTransitionsFromState(TKeyState stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains<TKeyState, TKeyState, TKeyTransition, TKeyData, State>(_states, stateName, this._logger, exceptions);

            var transitionsFromState = result ? Check.GetValuesWhere<TKeyTransition, TKeyState, TKeyTransition, TKeyData, Transition>
                (_transitions, (x) => object.Equals(x.StateFrom,stateName), this._logger, out result, exceptions)
                : new Dictionary<TKeyTransition, Transition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", stateName);


            return transitionsFromState;
        }

        internal Dictionary<TKeyTransition, Transition> _GetTransitionsFromState(State state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains<TKeyState, TKeyState, TKeyTransition, TKeyData, State>(_states, state, this._logger, exceptions);
            var transitionsFromState = result ? Check.GetValuesWhere<TKeyTransition, TKeyState, TKeyTransition, TKeyData, Transition>(_transitions, (x) => object.Equals(x.StateFrom, state.Name), this._logger, out result, exceptions) : new Dictionary<TKeyTransition, Transition>();
            
            if(withLog)
                _logger.LogDebug("Get transitions from state \"{NameState}\" ", state.Name);

            return transitionsFromState;
        }

        public Dictionary<TKeyTransition, Transition> GetTransitionsFromState(TKeyState stateName)
        {
            return _GetTransitionsFromState(stateName, out bool result,  true, true);
        }

        public Dictionary<TKeyTransition, Transition> GetTransitionsFromState(State state)
        {
            return _GetTransitionsFromState(state, out bool result, true, true);
        }

        public Dictionary<TKeyTransition, Transition> TryGetTransitionsFromState(TKeyState stateName, out bool result)
        {
            return _GetTransitionsFromState(stateName, out result, false, true);
        }

        public Dictionary<TKeyTransition, Transition> TryGetTransitionsFromState(State state, out bool result)
        {
            return _GetTransitionsFromState(state, out result,  false, true);
        }



        internal Dictionary<TKeyTransition, Transition> _GetTransitionsToState(TKeyState stateName, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains<TKeyState, TKeyState, TKeyTransition, TKeyData, State>(_states, stateName, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere<TKeyTransition, TKeyState, TKeyTransition, TKeyData, Transition>(_transitions, (x) => object.Equals(x.StateTo, stateName), this._logger, out result, exceptions): new Dictionary<TKeyTransition, Transition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", stateName);

            return transitionsToState;
        }

        internal Dictionary<TKeyTransition, Transition> _GetTransitionsToState(State state, out bool result, bool exceptions, bool withLog)
        {
            result = Check.Contains<TKeyState, TKeyState, TKeyTransition, TKeyData, State>(_states, state, this._logger, exceptions);
            var transitionsToState = result ? Check.GetValuesWhere<TKeyTransition, TKeyState, TKeyTransition, TKeyData, Transition>(_transitions, (x) => object.Equals(x.StateTo, state.Name), this._logger, out result, exceptions) : new Dictionary<TKeyTransition, Transition>();

            if(withLog)
                _logger.LogDebug("Get transitions to state \"{NameState}\" ", state.Name);

            return transitionsToState;
        }

        public Dictionary<TKeyTransition, Transition> GetTransitionsToState(TKeyState stateName)
        {
            return _GetTransitionsToState(stateName, out bool result, true, true);
        }

        public Dictionary<TKeyTransition, Transition> GetTransitionsToState(State state)
        {
            return _GetTransitionsToState(state, out bool result, true, true);
        }

        public Dictionary<TKeyTransition, Transition> TryGetTransitionsToState(TKeyState stateName, out bool result)
        {
            return _GetTransitionsToState(stateName, out result, false, true);
        }

        public Dictionary<TKeyTransition, Transition> TryGetTransitionsToState(State state, out bool result)
        {
            return _GetTransitionsToState(state, out result, false, true);
        }

    }
}
