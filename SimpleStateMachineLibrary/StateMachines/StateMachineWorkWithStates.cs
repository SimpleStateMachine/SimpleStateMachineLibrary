using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        internal State _GetState(string nameState, out bool result, bool exception)
        {
            var _state = Check.GetElement(_states, nameState, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get state \"{NameState}\"", nameState);
            else
                _logger?.LogDebug("Try get state \"{NameState}\"", nameState);

            return _state;
        }

        //private State _GetState(State state, out bool result, bool exception)
        //{
        //    var _state = Check.GetElement(_states, state, this._logger, out result, exception);

        //    if (exception)
        //        _logger?.LogDebug("Get state \"{NameState}\"", state.Name);
        //    else
        //        _logger?.LogDebug("Try get state \"{NameState}\"", state.Name);


        //    return _state;
        //}
        public bool StateExists(string nameState)
        {
            return Check.Contains(_states, nameState, this._logger, false);
        }

        public State GetState(string nameState)
        {
            return _GetState(nameState, out bool result, true);
        }

        public State TryGetState(string nameState, out bool result)
        {
            return _GetState(nameState, out result, false);
        }

        //public State TryGetState(State state, out bool result)
        //{
        //    return _GetState(state, out result, false);
        //}



        internal State _AddState(string nameState, Action<State, Dictionary<string, object>> actionOnEntry, Action<State, Dictionary<string, object>> actionOnExit, out bool result, bool exception)
        {
            //throw that element already contains  
            result = Check.NotContains(_states, nameState, this._logger, exception);

            
            if (!result)
                return null;

            return new State(this, nameState, actionOnEntry, actionOnExit);
        }

        internal State AddState(State state, out bool result, bool exception)
        {
            //throw that element already contains 
            result = Check.NotContains(_states, state, this._logger, exception);
         
            if (!result)
                return null;

            _states.Add(state.Name, state);

            if (exception)
                _logger?.LogDebug("Add state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try add state \"{NameState}\"", state.Name);

            return state;
        }

        internal State AddState(XElement xElement)
        {
            return SimpleStateMachineLibrary.State.FromXElement(this, Check.Object(xElement, this._logger));
        }


        public State AddState(string nameState, Action<State, Dictionary<string, object>> actionOnEntry = null, Action<State, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out bool result, true);
        }

        public State TryAddState(out bool result, string nameState, Action<State, Dictionary<string, object>> actionOnEntry = null, Action<State, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out result, false);
        }



        private State _DeleteState(State state, out bool result, bool exception)
        {
            
            var _state  = Check.Remove(_states, state, this._logger, out result, exception);


            if (exception)
                _logger?.LogDebug("Delete state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try delete state \"{NameState}\"", state.Name);

            return _state;
        }

        private State _DeleteState(string stateName, out bool result, bool exception)
        {
            var _state = Check.Remove(_states, stateName, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Delete state \"{NameState}\"", stateName);
            else
                _logger?.LogDebug("Try delete state \"{NameState}\"", stateName);

            return _state;
        }


        public State DeleteState(State state)
        {
            return _DeleteState(state, out bool result, true);
        }

        public State DeleteState(string stateName)
        {
            return _DeleteState(GetState(stateName), out bool result, true);
        }

        public State TryDeleteState(State state, out bool result)
        {
            return _DeleteState(state, out  result, false);
        }

        public State TryDeleteState(string stateName, out bool result)
        {
            return _DeleteState(stateName, out result, false);
        }
    }
}
