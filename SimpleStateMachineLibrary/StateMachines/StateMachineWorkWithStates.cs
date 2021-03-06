﻿using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        internal string _StateExists(string nameState, out bool result,  bool exeption, bool withLog)
        {
            return Check.Contains(_states, nameState, this._logger, out result, exeption);
        }

        public bool StateExists(string nameState)
        {
            nameState = _StateExists(nameState, out bool result, false, true);
            return result;
        }

        internal State _GetState(string nameState, out bool result, bool exception, bool withLog)
        {
            var _state = Check.GetElement(_states, nameState, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get state \"{NameState}\"", nameState);
                else
                    _logger.LogDebug("Try get state \"{NameState}\"", nameState);
            }

            return _state;
        }

        public State GetState(string nameState)
        {
            return _GetState(nameState, out bool result, true, true);
        }

        public State TryGetState(string nameState, out bool result)
        {
            return _GetState(nameState, out result, false, true);
        }

        internal State _AddState(string nameState, Action<State, Dictionary<string, object>> actionOnEntry, Action<State, Dictionary<string, object>> actionOnExit, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains  
            result = Check.NotContains(_states, nameState, this._logger, exception);

            
            if (!result)
                return null;

            return new State(this, nameState, actionOnEntry, actionOnExit, withLog);
        }

        internal State _AddState(State state, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(_states, state, this._logger, exception);
         
            if (!result)
                return null;

            _states.Add(state.Name, state);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add state \"{NameState}\"", state.Name);
                else
                    _logger.LogDebug("Try add state \"{NameState}\"", state.Name);
            }

            return state;
        }

        internal State _AddState(XElement xElement, bool withLog)
        {
            return State.FromXElement(this, Check.Object(xElement, this._logger), withLog);
        }


        public State AddState(string nameState, Action<State, Dictionary<string, object>> actionOnEntry = null, Action<State, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out bool result, true, true);
        }

        public State TryAddState(out bool result, string nameState, Action<State, Dictionary<string, object>> actionOnEntry = null, Action<State, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out result, false, true);
        }



        internal State _DeleteState(State state, out bool result, bool exception, bool withLog)
        {
            
            var _state  = Check.Remove(_states, state, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete state \"{NameState}\"", state.Name);
                else
                    _logger.LogDebug("Try delete state \"{NameState}\"", state.Name);
            }

            return _state;
        }

        internal State _DeleteState(string stateName, out bool result, bool exception, bool withLog)
        {
            var _state = Check.Remove(_states, stateName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete state \"{NameState}\"", stateName);
                else
                    _logger.LogDebug("Try delete state \"{NameState}\"", stateName);
            }

            return _state;
        }


        public State DeleteState(State state)
        {
            return _DeleteState(state, out bool result, true, true);
        }

        public State DeleteState(string stateName)
        {
            return _DeleteState(_GetState(stateName, out bool result, true, false), out result, true, true);
        }

        public State TryDeleteState(State state, out bool result)
        {
            return _DeleteState(state, out  result, false, true);
        }

        public State TryDeleteState(string stateName, out bool result)
        {
            return _DeleteState(stateName, out result, false, true);
        }
    }
}
