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

        internal ILogger _logger;

        public ILogger SetILogger(ILogger logger)
        {
            _logger = logger ?? NullLogger.Instance;

            return logger;
        }

        public State CurrentState { get; private set; }

        public State PreviousState { get; private set; }

        public Transition CurrentTransition{ get; private set; }

        public State StartState { get; private set; }


        public StateMachine(ILogger logger=null)
        {
            SetILogger(logger);
        }

        public StateMachine(XDocument xDocument, ILogger logger = null)
        {
            SetILogger(logger);
            FromXDocument(this, xDocument);
          
        }

        public StateMachine(string xDocumentPath, ILogger logger = null)
        {
            SetILogger(logger);
            FromXDocument(this, xDocumentPath);
        }


        internal Transition _nextTransition;

        internal Dictionary<string, object> _currentParameters;

        internal Dictionary<string, object> _nextParameters;

        internal Action<State, State> _onChangeState;


        public StateMachine OnChangeState(Action<State, State> actionOnChangeState)
        {
            _onChangeState += actionOnChangeState;

            return this;
        }

        public State SetStartState(State state)
        {
            StartState = state;
            return state;
        }
        
        public State SetStartState(string stateName)
        {
            StartState = State(stateName);
            return StartState;
        }

        public InvokeParameters InvokeTransition(string nameTransition)
        {
            _nextTransition = Check.GetElement(_transitions, nameTransition, true);

            if(_nextTransition.StateTo!=CurrentState)
            {
                throw new ArgumentException(message: $"{nameTransition} not available from {CurrentState?.Name} " );
            }

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
            CurrentState.Exit(_currentParameters);

            return this;
        }

        public void Start(Dictionary<string, object> startParameters = null)
        {        
            if(StartState==null)
            {
                throw new NullReferenceException(message: "Start state don't set");
            }

            CurrentState = StartState;
            _currentParameters = startParameters;

            ChangeState();

            while (_nextTransition != null)
            {
                InvokeTransition();

                ChangeState();
            }

        }

    }
}
