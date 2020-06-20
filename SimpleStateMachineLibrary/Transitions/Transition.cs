using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class Transition : NamedObject
    {
        public string StateFrom { get; private set; }

        public string StateTo { get; private set; }

        private Action<Transition, Dictionary<string, object>> _onInvoke;

        internal Transition(StateMachine stateMachine, string nameTransition, string stateFrom, string stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke, bool withLog) : base(stateMachine, nameTransition)
        {

            StateFrom = stateMachine._StateExists(stateFrom, out _, true, false);
            StateTo = stateMachine._StateExists(stateTo, out _, true, false);

            //stateMachine?._logger.LogDebug("Create transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", nameTransition, stateFrom, stateTo);

            stateMachine._AddTransition(this, out _, true, withLog);

            if (actionOnInvoke != null)
            {
                OnInvoke(actionOnInvoke);
            }      
        }

        public Transition Delete()
        {
            return this.StateMachine.DeleteTransition(this);
        }

        public Transition TryDelete(out bool result)
        {
            return this.StateMachine.TryDeleteTransition(this, out result);
        }

        public Transition OnInvoke(Action<Transition, Dictionary<string, object>> actionOnInvoke)
        {
            actionOnInvoke = Check.Object(actionOnInvoke, this.StateMachine?._logger);

            _onInvoke += actionOnInvoke;

            this.StateMachine._logger.LogDebug("Method \"{NameMethod}\" subscribe on invoke for transition \"{NameTransition}\"", actionOnInvoke.Method.Name, this.Name);

            return this;
        }

        public InvokeParameters Invoke(Dictionary<string, object> parameters)
        {
            return StateMachine.InvokeTransition(this, parameters);
        }
       
        internal Transition _Invoking(Dictionary<string, object> parameters)
        {
            _onInvoke?.Invoke (this, parameters);
            this.StateMachine._logger.LogDebug("Invoke transition \"{NameTransition}\"", this.Name);
            return this;
        }


    }
}
