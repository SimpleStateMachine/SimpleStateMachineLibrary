using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class State : NamedObject<State>
    {
        private Action<State, Dictionary<string, object>> _onEntry;

        private Action<State, Dictionary<string, object>> _onExit;

        protected internal State(StateMachine stateMachine, string nameState) : base(stateMachine, nameState)
        {

        }

        public State Delete()
        {
            return this.StateMachine.DeleteState(this);
        }

        public State TryDelete()
        {
            return this.StateMachine.TryDeleteState(this);
        }

        public State SetAsStartState()
        {
            this.StateMachine.SetStartState(this);
            return this;
        }

        public State OnEntry(Action<State, Dictionary<string, object>> actionOnEntryWithParameters)
        {
            _onEntry += actionOnEntryWithParameters;

            return this;
        }

        public State OnExit(Action<State, Dictionary<string, object>> actionOnExitWithoutParameters)
        {
            _onExit += actionOnExitWithoutParameters;

            return this;
        }

        internal void Entry(Dictionary<string, object> parameters)
        {
            _onEntry?.Invoke (this, parameters);
        }

        internal void Exit(Dictionary<string, object> parameters)
        {
            _onExit?.Invoke(this, parameters);
        }
    }
}
