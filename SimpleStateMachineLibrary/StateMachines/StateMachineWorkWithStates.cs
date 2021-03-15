using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        public bool StateExists(string nameState)
        {
            nameState = _StateExists(nameState, out var result, false, true);
            return result;
        }
        
        public IState GetState(string nameState)
        {
            return _GetState(nameState, out var result, true, true);
        }
        public IState TryGetState(string nameState, out bool result)
        {
            return _GetState(nameState, out result, false, true);
        }
        
        public IState AddState(string nameState, Action<IState, Dictionary<string, object>> actionOnEntry = null, Action<IState, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out var result, true, true);
        }
        public IState TryAddState(out bool result, string nameState, Action<IState, Dictionary<string, object>> actionOnEntry = null, Action<IState, Dictionary<string, object>> actionOnExit = null)
        {
            return _AddState(nameState, actionOnEntry, actionOnExit, out result, false, true);
        }
       
        public IState DeleteState(IState state)
        {
            return _DeleteState(state, out var result, true, true);
        }
        public IState DeleteState(string stateName)
        {
            return _DeleteState(_GetState(stateName, out var result, true, false), out result, true, true);
        }
        public IState TryDeleteState(IState state, out bool result)
        {
            return _DeleteState(state, out  result, false, true);
        }
        public IState TryDeleteState(string stateName, out bool result)
        {
            return _DeleteState(stateName, out result, false, true);
        }
        
        
        internal string _StateExists(string nameState, out bool result,  bool exeption, bool withLog)
        {
            return Check.Contains(States, nameState, _logger, out result, exeption);
        }
        
        internal IState _GetState(string nameState, out bool result, bool exception, bool withLog)
        {
            var _state = Check.GetElement(States, nameState, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get state \"{NameState}\"", nameState);
                else
                    _logger.LogDebug("Try get state \"{NameState}\"", nameState);
            }

            return _state;
        }
        
        internal IState _AddState(string nameState, Action<IState, Dictionary<string, object>> actionOnEntry, Action<IState, Dictionary<string, object>> actionOnExit, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains  
            result = Check.NotContains(States, nameState, this._logger, exception);

            
            if (!result)
                return null;

            return new IState(this, nameState, actionOnEntry, actionOnExit, withLog);
        }
        internal IState _AddState(IState state, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(States, state, this._logger, exception);
         
            if (!result)
                return null;

            States.Add(state.Name, state);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add state \"{NameState}\"", state.Name);
                else
                    _logger.LogDebug("Try add state \"{NameState}\"", state.Name);
            }

            return state;
        }
        internal IState _AddState(XElement xElement, bool withLog)
        {
            return IState.FromXElement(this, Check.Object(xElement, this._logger), withLog);
        }
        
        internal IState _DeleteState(IState state, out bool result, bool exception, bool withLog)
        {
            
            var _state  = Check.Remove(States, state, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete state \"{NameState}\"", state.Name);
                else
                    _logger.LogDebug("Try delete state \"{NameState}\"", state.Name);
            }

            return _state;
        }
        internal IState _DeleteState(string stateName, out bool result, bool exception, bool withLog)
        {
            var _state = Check.Remove(States, stateName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete state \"{NameState}\"", stateName);
                else
                    _logger.LogDebug("Try delete state \"{NameState}\"", stateName);
            }

            return _state;
        }
        
    }
}
