using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;


namespace SimpleStateMachineLibrary
{
    public partial class Transition : NamedObject
    {
        public State StateFrom { get; protected set; }

        public State StateTo { get; protected set; }

        private Action<StateMachine, Dictionary<string, object>> _onInvokeWithParameters;

        private Action<StateMachine> _onInvokeWithoutParameters;

        private void CheckOnInvokeFunc()
        {
            if ((_onInvokeWithParameters != null) || (_onInvokeWithoutParameters != null))
            {
                throw new ArgumentException(String.Format("Func on Invoke for Transition with name {0} already set", this.Name));
            }
        }

        public Transition OnInvoke(Action<StateMachine> actionOnTransitionWithoutParameters)
        {
            CheckOnInvokeFunc();

            _onInvokeWithoutParameters = actionOnTransitionWithoutParameters;

            return this;
        }

        public Transition OnInvoke(Action<StateMachine, Dictionary<string, object>> actionOnTransitionWithParameters)
        {
            CheckOnInvokeFunc();

            _onInvokeWithParameters = actionOnTransitionWithParameters;

            return this;
        }


        public Transition(StateMachine stateMachine, string nameTransition, State stateFrom, State stateTo) : base(stateMachine, nameTransition)
        {         
            StateFrom = stateFrom;
            StateTo = stateTo;
        }

        public Transition Delete()
        {
            return this.StateMachine.DeleteTransition(this);
        }

        public Transition TryDelete()
        {
            return this.StateMachine.TryDeleteTransition(this);
        }

        internal void Invoke(Dictionary<string, object> parameters)
        {

            if (_onInvokeWithParameters!=null)
            {
                _onInvokeWithParameters(this.StateMachine,parameters);
            }
            else if (_onInvokeWithoutParameters != null)
            {
                _onInvokeWithoutParameters(this.StateMachine);
            }
        }

    }
}
