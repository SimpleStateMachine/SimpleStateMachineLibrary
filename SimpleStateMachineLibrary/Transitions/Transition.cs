using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public partial class Transition : NamedObject<Transition>
    {
        public State StateFrom { get; protected set; }

        public State StateTo { get; protected set; }

        private Action<Transition, Dictionary<string, object>> _onInvoke;

        protected internal Transition(StateMachine stateMachine, string nameTransition, State stateFrom, State stateTo) : base(stateMachine, nameTransition)
        {         
            StateFrom = stateFrom;
            StateTo = stateTo;
            stateMachine.AddTransition(this, true);
        }

        public Transition Delete()
        {
            return this.StateMachine.DeleteTransition(this);
        }

        public Transition TryDelete()
        {
            return this.StateMachine.TryDeleteTransition(this);
        }

        public Transition OnInvoke(Action<Transition, Dictionary<string, object>> actionOnTransitionWithParameters)
        {
            _onInvoke += actionOnTransitionWithParameters;

            return this;
        }

        internal void Invoke(Dictionary<string, object> parameters)
        {
            _onInvoke?.Invoke (this, parameters);
        }


    }
}
