using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class State : NamedObject, IState
    {
        private Action<IState, Dictionary<string, object>> _onEntry;

        private Action<IState, Dictionary<string, object>> _onExit;

        internal State(StateMachine stateMachine, string nameState, Action<IState, Dictionary<string, object>> actionOnEntry, Action<IState, Dictionary<string, object>> actionOnExit, bool withLog) : base(stateMachine, nameState)
        {
            //stateMachine?._logger.LogDebug("Create state \"{NameState}\" ", nameState);

            StateMachine._AddState(this, out var result, true, withLog);

            if (actionOnEntry != null)
            {
                OnEntry(actionOnEntry);
            }

            if (actionOnExit != null)
            {
                OnExit(actionOnExit);
            }
            
        }
        
        public State OnEntry(Action<IState, Dictionary<string, object>> actionOnEntry)
        {

            actionOnEntry = Check.Object(actionOnEntry, this.StateMachine?._logger);

            _onEntry += actionOnEntry;
            this.StateMachine._logger.LogDebug("Method \"{NameMethod}\" subscribe on entry for state \"{NameState}\"", actionOnEntry.Method.Name, this.Name);
            return this;
        }

        public State OnExit(Action<IState, Dictionary<string, object>> actionOnExit)
        {
            actionOnExit = Check.Object(actionOnExit, this.StateMachine?._logger);

            _onExit += actionOnExit;            
            this.StateMachine._logger.LogDebug("Method \"{NameMethod}\" subscribe on exit for state \"{NameState}\"", actionOnExit.Method.Name, this.Name);
            return this;
        }

        internal void _Entry(Dictionary<string, object> parameters, bool withLog)
        {
            _onEntry?.Invoke (this, parameters);

            if(withLog)
                this.StateMachine._logger.LogDebug("Entry to state \"{NameState}\"",  this.Name);
        }

        internal void _Exit(Dictionary<string, object> parameters, bool withLog)
        {
            _onExit?.Invoke(this, parameters);

            if (withLog)
                this.StateMachine._logger.LogDebug("Exit from state \"{NameState}\"", this.Name);
        }
    }
}
