using SimpleStateMachineLibrary.Helpers;
using SimpleStateMachineLibrary.StateMachines;
using SimpleStateMachineLibrary.Transitions;
using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary.States
{

    public partial class State : NamedObject
    {
        private Action<StateMachine, Dictionary<string, object>> _actionOnEntryWithParameters;
        private Action<StateMachine> _actionOnEntryWithoutParameters;
        private Action<StateMachine, Dictionary<string, object>> _actionOnExitWithParameters;
        private Action<StateMachine> _actionOnExitWithoutParameters;
        
        public State(StateMachine stateMachine, string nameState) : base(stateMachine, nameState)
        {

        }

        private void CheckOnEntryFunc()
        {
            if((_actionOnEntryWithParameters != null)|| (_actionOnEntryWithoutParameters != null))
            {
                throw new ArgumentException(String.Format("Func on Entry for State with name {0} already set", this.Name));
            }
        }

        private void CheckOnExitFunc()
        {
            if ((_actionOnExitWithParameters != null) || (_actionOnExitWithoutParameters != null))
            {
                throw new ArgumentException(String.Format("Func on Exit for State with name {0} already set", this.Name));
            }
        }

        public State OnEntry(Action<StateMachine> actionOnEntryWithoutParameters)
        {
            CheckOnEntryFunc();

            _actionOnEntryWithoutParameters = actionOnEntryWithoutParameters;

            return this;
        }

        public State OnEntry(Action<StateMachine, Dictionary<string, object>> actionOnEntryWithParameters)
        {
            CheckOnEntryFunc();

            _actionOnEntryWithParameters = actionOnEntryWithParameters;

            return this;
        }

        public State OnExit(Action<StateMachine> actionOnExitWithoutParameters)
        {
            CheckOnExitFunc();

            _actionOnExitWithoutParameters = actionOnExitWithoutParameters;

            return this;
        }

        public State OnExit(Action<StateMachine, Dictionary<string, object>> actionOnExitWithParameters)
        {
            CheckOnExitFunc();

            _actionOnExitWithParameters = actionOnExitWithParameters;

            return this;
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

        public State SetAsEndState()
        {
            this.StateMachine.SetEndState(this);
            return this;
        }

        internal void Entry(Dictionary<string, object> parameters)
        {

            if (_actionOnEntryWithParameters != null)
            {
               _actionOnEntryWithParameters(this.StateMachine, parameters);
            }
            else if (_actionOnEntryWithoutParameters != null)
            {
                _actionOnEntryWithoutParameters(this.StateMachine);
            }

        }

        internal void Exit(Dictionary<string, object> parameters)
        {
            if (_actionOnExitWithParameters != null)
            {
                 _actionOnExitWithParameters(this.StateMachine, parameters);
            }
            else if (_actionOnExitWithoutParameters != null)
            {
                 _actionOnExitWithoutParameters(this.StateMachine);
            }

        }
    }
}
