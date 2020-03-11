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

        public State CurrentState { get; private set; }

        public State PreviousState { get; private set; }

        public Transition CurrentTransition{ get; private set; }

        public State StartState { get; private set; }

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

        internal Transition _nextTransition;

        internal Dictionary<string, object> _currentParameters;

        internal Dictionary<string, object> _nextParameters;

        internal Action<State, State> _onChangeState;


        public StateMachine OnChangeState(Action<State, State> actionOnChangeState)
        {
            _onChangeState += actionOnChangeState;
            _logger?.LogDebug("Method \"{NameMethod}\" subscribe on change state State Machine", actionOnChangeState.Method.Name);
            return this;
        }

        public State SetStartState(State state)
        {
            StartState = state;

            _logger?.LogDebug("State \"{NameState}\" set as start", state.Name);

            return state;
        }
        
        public State SetStartState(string stateName)
        {
            StartState = GetState(stateName);

            _logger?.LogDebug("State \"{NameState}\" set as start", stateName);

            return StartState;
        }

        public InvokeParameters InvokeTransition(string nameTransition)
        {
            _nextTransition = Check.GetElement(_transitions, nameTransition, this._logger, out bool result, true);

            if(_nextTransition.StateFrom!=CurrentState)
            {
                object[] args = { nameTransition, CurrentState?.Name };
                string message = "Transition \"{0}\" not available from state \"{0}\"";
                var exception = new ArgumentException(message: String.Format(message, args));
                _logger?.LogError(exception, message, args);

                throw exception;
            }
            _logger?.LogDebug("Transition \"{NameTransition}\" set as next", nameTransition);

            return new InvokeParameters(this);
        }

        public InvokeParameters InvokeTransitionWithParameters(string nameTransition, Dictionary<string, object> parameters)
        {        
            InvokeTransition(nameTransition);

            _nextParameters = parameters;

            return new InvokeParameters(this);
        }

        private StateMachine InvokeTransition()
        {

            //Mark nextParameters as current
            _currentParameters = _nextParameters;
            _nextParameters = null;

            //Mark nextTransition as current
            CurrentTransition = _nextTransition;
            _nextTransition = null;

            //Mark currentState as previous
            PreviousState = CurrentState;
            CurrentState = null;

            
            CurrentTransition.Invoke(_currentParameters);
            CurrentState = CurrentTransition.StateTo;
            CurrentTransition = null;

            return this;
        }

        private StateMachine ChangeState()
        {
            CurrentState.Entry(_currentParameters);

            _onChangeState?.Invoke(PreviousState, CurrentState);

            object[] obj = { PreviousState?.Name, CurrentState?.Name };

            _logger?.LogDebug("State \"{StateOld}\" change on  \"{StateNew}\"", obj);
            
            CurrentState.Exit(_currentParameters);

            return this;
        }

        public void Start(Dictionary<string, object> startParameters = null)
        {        
            if(StartState==null)
            {
                string message = "Start state not set";
                var exception = new NullReferenceException(message: message);
                _logger?.LogError(exception, message);
                throw exception;
                
            }

            _logger?.LogDebugAndInformation("Start work state machine");

            CurrentState = StartState;
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
