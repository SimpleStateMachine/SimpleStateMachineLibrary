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

    public partial class StateMachine: IStateMachine
    {
        private Dictionary<string, IState> States { get; } = new Dictionary<string, IState>();
        private Dictionary<string, ITransition> Transitions { get; }  = new Dictionary<string, ITransition>();
        private Dictionary<string, IData> Data { get; }  = new Dictionary<string, IData>();

        public IState CurrentState { get; private set; }

        public IState PreviousState { get; private set; }
        
        public ITransition CurrentTransition { get; private set; }
        
        public ITransition NextTransition { get; private set; }
        
        public IState StartState { get; private set; }

        internal Dictionary<string, object> _currentParameters;

        internal Dictionary<string, object> _nextParameters;

        internal Action<State, State> _onChangeState;
        


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
            _FromXDocument(this, xDocument, true);
          
        }

        public StateMachine(string xDocumentPath, ILogger logger = null): this(logger)
        {
            _FromXDocument(this, xDocumentPath, true);
        }

        public StateMachine OnChangeState(Action<State, State> actionOnChangeState)
        {
            _onChangeState += actionOnChangeState;
            _logger.LogDebug("Method \"{NameMethod}\" subscribe on change state for State Machine", actionOnChangeState.Method.Name);
            return this;
        }

        public State SetStartState(State state)
        {
            _startState = _StateExists(state.Name, out _, true, false);

            _logger.LogDebug("State \"{NameState}\" set as start", state.Name);

            return state;
        }
        
        public State SetStartState(string stateName)
        {
            var state = _GetState(stateName, out var result, true, false);
            StartState = state;

            if(result)
                _logger.LogDebug("State \"{NameState}\" set as start", stateName);

            return state;
        }

        public InvokeParameters InvokeTransition(string nameTransition, Dictionary<string, object> parameters=null)
        {
            _nextTransition = _TransitionExists(nameTransition, out _,true, false);

            _CheckBeforeInvoke(this._logger, true);

            var invokeParameters = new InvokeParameters(this);
            
            invokeParameters.AddParameters(parameters);
            
            return invokeParameters;
        }

        internal void _CheckBeforeInvoke(ILogger logger, bool withLog)
        {
            if (NextTransition?.StateFrom!= CurrentState)
            {
                object[] args = { NextTransition.Name, CurrentState.Name  };
                var message = "Transition \"{0}\" not available from state \"{0}\"";
                var exception = new ArgumentException(message: String.Format(message, args));
                _logger.LogError(exception, message, args);

                throw exception;
            }

            if(withLog)
                _logger.LogDebug("Transition \"{NameTransition}\" set as next", NextTransition.Name);
        }

        public InvokeParameters InvokeTransition(Transition transition, Dictionary<string, object> parameters = null)
        {
            return InvokeTransition(transition?.Name, parameters);
        }


        internal StateMachine _InvokeTransition()
        {

            //Mark nextParameters as current
            _currentParameters = _nextParameters;
            _nextParameters = null;

            //Mark nextTransition as current
            CurrentTransition = NextTransition;
            NextTransition = null;

            //Mark currentState as previous
            PreviousState = CurrentState;
            CurrentState = null;
            
            CurrentTransition._Invoking(_currentParameters);
            CurrentState = CurrentTransition.StateTo;
            CurrentTransition = null;

            return this;
        }

        internal StateMachine _ChangeState()
        {
            var currentState = _GetState(CurrentStateName, out var result, true, false);
            currentState._Entry(_currentParameters, true);
            State previousState = null;
            var obj = new List<object>(); 
            string message;

            if (string.IsNullOrEmpty(PreviousStateName))
            {
                obj.Add(CurrentStateName);
                 message = "State \"{StateNew}\" was set";
            }
            else
            {
                obj.Add(PreviousStateName);
                obj.Add(CurrentStateName);
                message = "State \"{StateOld}\" change on  \"{StateNew}\"";
                previousState = _GetState(PreviousStateName, out _, true, false);
            }

            _onChangeState?.Invoke(previousState, currentState);
            _logger.LogDebug(message, obj.ToArray());
            currentState._Exit(_currentParameters, true);

            return this;
        }

        internal void _CheckStartState()
        {
            string message;
            if (string.IsNullOrEmpty(_startState))
            {
                message = "Start state not set";
                var exception = new NullReferenceException(message: message);
                _logger.LogError(exception, message);
                throw exception;
            }
            _startState = _StateExists(_startState, out _, true, false);          
            CurrentStateName = _startState;        
        }

        public void Start(Dictionary<string, object> startParameters = null)
        {
            _CheckStartState();
     
            _logger.LogInformation("Start work state machine");
            _currentParameters = startParameters;

            _ChangeState();

            while (NextTransition != null)
            {
                _InvokeTransition();

                _ChangeState();
            }
            _logger.LogInformation("End work state machine");

        }
        
    }
}
