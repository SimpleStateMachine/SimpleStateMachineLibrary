using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private State _State(string nameState, bool exeption)
        {
            return Check.GetElement(_states, nameState, exeption);
        }

        private State _State(State state, bool exeption)
        {
            return Check.GetElement(_states, state, exeption);
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
            return Check.Remove(_states, state, exeption);
        }

        private State _DeleteState(string stateName, bool exeption)
        {
            return Check.Remove(_states, stateName, exeption);
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
