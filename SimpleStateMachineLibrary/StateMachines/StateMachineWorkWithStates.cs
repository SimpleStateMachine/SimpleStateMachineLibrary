using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private State _State(string nameState, bool exeption)
        {
            var _state = Check.GetElement(_states, nameState, exeption);

            if (exeption)
                _logger?.LogDebug("Get state \"{NameState}\"", nameState);
            else
                _logger?.LogDebug("Try get state \"{NameState}\"", nameState);

            return _state;
        }

        private State _State(State state, bool exeption)
        {
            var _state = Check.GetElement(_states, state, exeption);

            if (exeption)
                _logger?.LogDebug("Get state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try get state \"{NameState}\"", state.Name);


            return _state;
        }

        public State State(string nameState, bool exeption = true)
        {
            return _State(nameState, exeption);
        }

        public State TryGetState(string nameState)
        {
            return _State(nameState, false);
        }

        public State TryGetState(State state)
        {
            return _State(state, false);
        }

        private State _AddState(string nameState, bool exeption)
        {
            if (!Check.NotContains(_states, nameState, exeption))
                return null;

            return new State(this, nameState);
        }
        internal State AddState(State state, bool exeption)
        {
            if (!Check.NotContains(_states, state, exeption))
                return null;

            _states.Add(state.Name, state);

            if (exeption)
                _logger?.LogDebug("Add state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try add state \"{NameState}\"", state.Name);

            return state;
        }

        public State AddState(string nameState)
        {
            return _AddState(nameState, true);
        }

        public State AddState(XElement xElement)
        {
            return SimpleStateMachineLibrary.State.FromXElement(this, Check.Object(xElement));
        }

        public State TryAddState(string nameState)
        {
            return _AddState(nameState, false);
        }


        private State _DeleteState(State state, bool exeption)
        {
            var _state  = Check.Remove(_states, state, exeption);


            if (exeption)
                _logger?.LogDebug("Delete state \"{NameState}\"", state.Name);
            else
                _logger?.LogDebug("Try delete state \"{NameState}\"", state.Name);

            return _state;
        }

        private State _DeleteState(string stateName, bool exeption)
        {
            var _state = Check.Remove(_states, stateName, exeption);

            if (exeption)
                _logger?.LogDebug("Delete state \"{NameState}\"", stateName);
            else
                _logger?.LogDebug("Try delete state \"{NameState}\"", stateName);

            return _state;
        }

        public State DeleteState(State state)
        {
            return _DeleteState(state, true);
        }

        public State DeleteState(string stateName)
        {
            return _DeleteState(State(stateName), true);
        }

        public State TryDeleteState(State state)
        {
            return _DeleteState(state, false);
        }

        public State TryDeleteState(string stateName)
        {
            return _DeleteState(stateName, false);
        }
    }
}
