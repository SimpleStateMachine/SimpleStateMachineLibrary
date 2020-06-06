using System.Collections.Generic;
using System.Xml.Linq;
using System;
using SimpleStateMachineLibrary.Helpers;
using System.Data.Common;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace SimpleStateMachineLibrary
{

    public partial class StateMachine
    {
       
        private Dictionary<string, State> _states = new Dictionary<string, State>();

        private Dictionary<string, Transition> _transitions  = new Dictionary<string, Transition>();

        private Dictionary<string, Data> _data  = new Dictionary<string, Data>();

        internal string _currentState;

        internal string _previousState;
        internal string _currentTransition { get; private set; }

        internal string _nextTransition;
        internal string _startState { get; private set; }

        internal Dictionary<string, object> _currentParameters;

        internal Dictionary<string, object> _nextParameters;

        internal Action<State, State> _onChangeState;

        public State CurrentState { get { return GetState(_currentState); } }
        public Transition CurrentTransition { get { return GetTransition(_currentTransition); } }






        internal ILogger _logger;

        public ILogger SetLogger(ILogger logger)
        {
            _logger = logger ?? NullLogger.Instance;

            return logger;
        }

        public StateMachine(ILogger logger=null)
        {
            SetLogger(logger);
            _logger.LogDebug("Create state machine");
        }

        public StateMachine(XDocument xDocument, ILogger logger = null) : this(logger)
        {
            FromXDocument(this, xDocument);
          
        }

        public StateMachine(string xDocumentPath, ILogger logger = null): this(logger)
        {
            FromXDocument(this, xDocumentPath);
        }

        public StateMachine OnChangeState(Action<State, State> actionOnChangeState)
        {
            _onChangeState += actionOnChangeState;
            _logger?.LogDebug("Method \"{NameMethod}\" subscribe on change state State Machine", actionOnChangeState.Method.Name);
            return this;
        }

        public State SetStartState(State state)
        {
            _startState = _StateExists(state.Name, out _, true);

            _logger?.LogDebug("State \"{NameState}\" set as start", state.Name);

            return state;
        }
        
        public State SetStartState(string stateName)
        {
            State state = GetState(stateName);
            _startState = state.Name;

            _logger?.LogDebug("State \"{NameState}\" set as start", stateName);

            return state;
        }

        public InvokeParameters InvokeTransition(string nameTransition, Dictionary<string, object> parameters=null)
        {
            _nextTransition = TransitionExists(nameTransition, out _);

            CheckBeforeInvoke(this._logger);

            InvokeParameters invokeParameters = new InvokeParameters(this);
            if(parameters!=null)
                invokeParameters.AddParameters(parameters);
            return invokeParameters;
        }

        private void CheckBeforeInvoke(ILogger logger)
        {
            Transition transition = GetTransition(_nextTransition);
            if (transition.StateFrom!= _currentState)
            {
                object[] args = { _nextTransition, _currentState };
                string message = "Transition \"{0}\" not available from state \"{0}\"";
                var exception = new ArgumentException(message: String.Format(message, args));
                _logger?.LogError(exception, message, args);

                throw exception;
            }
            _logger?.LogDebug("Transition \"{NameTransition}\" set as next", _nextTransition);
        }

        public InvokeParameters InvokeTransition(Transition transition, Dictionary<string, object> parameters = null)
        {
            return InvokeTransition(transition?.Name, parameters);
        }


        private StateMachine InvokeTransition()
        {

            //Mark nextParameters as current
            _currentParameters = _nextParameters;
            _nextParameters = null;

            //Mark nextTransition as current
            _currentTransition = _nextTransition;
            _nextTransition = null;

            //Mark currentState as previous
            _previousState = _currentState;
            _currentState = null;

            Transition currentTransition = GetTransition(_currentTransition);
            currentTransition.Invoking(_currentParameters);
            _currentState = currentTransition.StateTo;
            _currentTransition = null;

            return this;
        }

        private StateMachine ChangeState()
        {
            State currentState = GetState(_currentState);
            currentState.Entry(_currentParameters);
            State previousState = null;
            object[] obj = { _previousState, _currentState };
            string message;
            if (string.IsNullOrEmpty(_previousState))
            {
                message = "State \"{StateNew}\" was set";
            }
            else
            {
                message = "State \"{StateOld}\" change on  \"{StateNew}\"";
                previousState = GetState(_previousState);
            }      
            _onChangeState?.Invoke(previousState, currentState);
            _logger?.LogDebug(message, obj);
            currentState.Exit(_currentParameters);

            return this;
        }

        private void CheckStartState()
        {
            string message;
            if (string.IsNullOrEmpty(_startState))
            {
                message = "Start state not set";
                var exception = new NullReferenceException(message: message);
                _logger?.LogError(exception, message);
                throw exception;
            }
            _startState = _StateExists(_startState, out _, true);          
            _currentState = _startState;        
        }

        public void Start(Dictionary<string, object> startParameters = null)
        {
            CheckStartState();
     
            _logger?.LogDebugAndInformation("Start work state machine");
            _currentParameters = startParameters;

            ChangeState();

            while (_nextTransition != null)
            {
                InvokeTransition();

                ChangeState();
            }
            _logger?.LogDebugAndInformation("End work state machine");

        }

    }
}
