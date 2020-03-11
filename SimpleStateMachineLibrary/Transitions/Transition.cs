using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class Transition : NamedObject
    {
        public State StateFrom { get; protected set; }

        public State StateTo { get; protected set; }

        private Action<Transition, Dictionary<string, object>> _onInvoke;

        protected internal Transition(StateMachine stateMachine, string nameTransition, State stateFrom, State stateTo) : base(stateMachine, nameTransition)
        {         
            StateFrom = stateFrom;
            StateTo = stateTo;

            stateMachine?._logger?.LogDebug("Create transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", nameTransition, stateFrom.Name, stateTo.Name);

            stateMachine.AddTransition(this, out bool result, true);
        }

        public Transition Delete()
        {
            return this.StateMachine.DeleteTransition(this);
        }

        public Transition TryDelete(out bool result)
        {
            return this.StateMachine.TryDeleteTransition(this, out result);
        }

        public Transition OnInvoke(Action<Transition, Dictionary<string, object>> actionOnTransitionWithParameters)
        {
            _onInvoke += actionOnTransitionWithParameters;

            this.StateMachine._logger?.LogDebug("Method \"{NameMethod}\" subscribe on invore for transition \"{NameTransition}\"", actionOnTransitionWithParameters.Method.Name, this.Name);

            return this;
        }

        internal Transition Invoke(Dictionary<string, object> parameters)
        {
            _onInvoke?.Invoke (this, parameters);
            this.StateMachine._logger?.LogDebugAndInformation("Invoke transition \"{NameTransition}\"", this.Name);
            return this;
        }


    }
}
