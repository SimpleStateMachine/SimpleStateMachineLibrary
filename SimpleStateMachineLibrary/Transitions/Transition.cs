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

        internal Transition(StateMachine stateMachine, string nameTransition, string stateFrom, string stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke) : base(stateMachine, nameTransition)
        {

            StateFrom = stateMachine._StateExists(stateFrom, out _, true);
            StateTo = stateMachine._StateExists(stateTo, out _, true);

            stateMachine?._logger?.LogDebug("Create transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", nameTransition, stateFrom, stateTo);

            if (actionOnInvoke != null)
            {
                OnInvoke(actionOnInvoke);
            }

            stateMachine.AddTransition(this, out _, true);
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

            this.StateMachine._logger?.LogDebug("Method \"{NameMethod}\" subscribe on invore for transition \"{NameTransition}\"", actionOnInvoke.Method.Name, this.Name);

            return this;
        }

        public InvokeParameters Invoke(Dictionary<string, object> parameters)
        {
            return StateMachine.InvokeTransition(this, parameters);
        }
       
        internal Transition Invoking(Dictionary<string, object> parameters)
        {
            _onInvoke?.Invoke (this, parameters);
            this.StateMachine._logger?.LogDebugAndInformation("Invoke transition \"{NameTransition}\"", this.Name);
            return this;
        }


    }
}
