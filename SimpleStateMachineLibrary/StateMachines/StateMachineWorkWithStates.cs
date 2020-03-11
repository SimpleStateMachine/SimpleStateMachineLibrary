using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private State _State(string nameState, out bool result, bool exception)
        {
            var _state = Check.GetElement(_states, nameState, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get state \"{NameState}\"", nameState);
            else
                _logger?.LogDebug("Try get state \"{NameState}\"", nameState);

            return _state;
        }

        private State _State(State state, out bool result, bool exception)
        {
            var _state = Check.GetElement(_states, state, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try get state \"{NameState}\"", state.Name);


            return _state;
        }

        public State State(string nameState, bool exception = true)
        {
            return _State(nameState, out bool result, exception);
        }

        public State TryGetState(string nameState, out bool result)
        {
            return _State(nameState, out result, false);
        }

        public State TryGetState(State state, out bool result)
        {
            return _State(state, out result, false);
        }

        private State _AddState(string nameState, out bool result, bool exception)
        {
            //throw that element already contains  
            result = Check.NotContains(_states, nameState, this._logger, exception);

            
            if (!result)
                return null;

            return new State(this, nameState);
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

        public State AddState(string nameState)
        {
            return _AddState(nameState, out bool result, true);
        }

        public State AddState(XElement xElement)
        {
            return SimpleStateMachineLibrary.State.FromXElement(this, Check.Object(xElement, this._logger));
        }

        public State TryAddState(string nameState, out bool result)
        {
            return _AddState(nameState, out result, false);
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
            return _DeleteState(State(stateName), out bool result, true);
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
