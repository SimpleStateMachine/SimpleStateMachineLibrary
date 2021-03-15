using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class Transition : NamedObject, ITransition
    {
        public IState StateFrom { get; private set; }

        public IState StateTo { get; private set; }

        private Action<ITransition, Dictionary<string, object>> _onInvoke;

        internal Transition(StateMachine stateMachine, string nameTransition, string stateFrom, string stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke, bool withLog) : base(stateMachine, nameTransition)
        {
            StateFrom = stateMachine._GetState(stateFrom, out _, true, false);
            StateTo = stateMachine._GetState(stateTo, out _, true, false);
            
            stateMachine._AddTransition(this, out _, true, withLog);

            if (actionOnInvoke != null)
            {
                OnInvoke(actionOnInvoke);
            }      
        }

        public Transition OnInvoke(Action<ITransition, Dictionary<string, object>> actionOnInvoke)
        {
            actionOnInvoke = Check.Object(actionOnInvoke, this.StateMachine?._logger);

            _onInvoke += actionOnInvoke;

            this.StateMachine._logger.LogDebug("Method \"{NameMethod}\" subscribe on invoke for transition \"{NameTransition}\"", actionOnInvoke.Method.Name, this.Name);

            return this;
        }
        
        internal ITransition _Invoking(Dictionary<string, object> parameters)
        {
            _onInvoke?.Invoke (this, parameters);
            this.StateMachine._logger.LogDebug("Invoke transition \"{NameTransition}\"", this.Name);
            return this;
        }


    }
}
