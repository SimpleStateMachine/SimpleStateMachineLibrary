using Microsoft.Extensions.Logging;
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
            stateMachine._logger?.LogDebug("Create state \"{NameState}\" ", nameState);

            StateMachine.AddState(this, true);
           
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
            this.StateMachine._logger?.LogDebug("Method \"{NameMethod}\" subscribe on entry for state \"{NameState}\"", actionOnEntryWithParameters.Method.Name, this.Name);
            return this;
        }

        public State OnExit(Action<State, Dictionary<string, object>> actionOnExitWithoutParameters)
        {           
            _onExit += actionOnExitWithoutParameters;
            this.StateMachine._logger?.LogDebug("Method \"{NameMethod}\" subscribe on exit for state \"{NameState}\"", actionOnExitWithoutParameters.Method.Name, this.Name);
            return this;
        }

        internal void Entry(Dictionary<string, object> parameters)
        {
            _onEntry?.Invoke (this, parameters);
            this.StateMachine._logger?.LogDebug("Entry to state \"{NameState}\"",  this.Name);
        }

        internal void Exit(Dictionary<string, object> parameters)
        {
            _onExit?.Invoke(this, parameters);
            this.StateMachine._logger?.LogDebug("Exit from state \"{NameState}\"", this.Name);
        }
    }
}
