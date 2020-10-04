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

    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>: IStateMachine
    {
       
        private Dictionary<TKeyState, State> _states = new Dictionary<TKeyState, State>();

        private Dictionary<TKeyTransition, Transition> _transitions  = new Dictionary<TKeyTransition, Transition>();

        private Dictionary<TKeyData, Data> _data  = new Dictionary<TKeyData, Data>();

        public TKeyState CurrentStateName { get; private set; }

        public TKeyState PreviousStateName { get; private set; }
        public TKeyTransition CurrentTransitionName { get; private set; }

        internal TKeyTransition _nextTransition;
        internal TKeyState _startState { get; private set; }

        internal Dictionary<string, object> _currentParameters;

        internal Dictionary<string, object> _nextParameters;

        internal Action<State, State> _onChangeState;

        public State CurrentState { get { return _GetState(CurrentStateName, out _, true, false); } }
        public Transition CurrentTransition { get { return _GetTransition(CurrentTransitionName, out _, true, false); } }
        public State PreviousState { get { return _GetState(PreviousStateName, out _, true, false); } }




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

        public StateMachine<TKeyState, TKeyTransition, TKeyData> OnChangeState(Action<State, State> actionOnChangeState)
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
        
        public State SetStartState(TKeyState stateName)
        {
            State state = _GetState(stateName, out bool result, true, false);
            _startState = state.Name;

            if(result)
                _logger.LogDebug("State \"{NameState}\" set as start", stateName);

            return state;
        }

        public InvokeParameters<TKeyState, TKeyTransition, TKeyData> InvokeTransition(TKeyTransition nameTransition, Dictionary<string, object> parameters=null)
        {
            _nextTransition = _TransitionExists(nameTransition, out _,true, false);

            _CheckBeforeInvoke(this._logger, true);

            InvokeParameters<TKeyState, TKeyTransition, TKeyData> invokeParameters = new InvokeParameters<TKeyState, TKeyTransition, TKeyData>(this);
            if(parameters!=null)
                invokeParameters.AddParameters(parameters);
            return invokeParameters;
        }

        internal void _CheckBeforeInvoke(ILogger logger, bool withLog)
        {
            Transition transition = _GetTransition(_nextTransition, out _, true, false);
            if (!object.Equals(transition.StateFrom,CurrentStateName))
            {
                object[] args = { _nextTransition, CurrentStateName };
                string message = "Transition \"{0}\" not available from state \"{0}\"";
                var exception = new ArgumentException(message: String.Format(message, args));
                _logger.LogError(exception, message, args);

                throw exception;
            }

            if(withLog)
                _logger.LogDebug("Transition \"{NameTransition}\" set as next", _nextTransition);
        }

        public InvokeParameters<TKeyState, TKeyTransition, TKeyData> InvokeTransition(Transition transition, Dictionary<string, object> parameters = null)
        {
            return InvokeTransition(transition==null?default:transition.Name, parameters);
        }


        internal StateMachine<TKeyState, TKeyTransition, TKeyData> _InvokeTransition()
        {

            //Mark nextParameters as current
            _currentParameters = _nextParameters;
            _nextParameters = default;

            //Mark nextTransition as current
            CurrentTransitionName = _nextTransition;
            _nextTransition = default;

            //Mark currentState as previous
            PreviousStateName = CurrentStateName;
            CurrentStateName = default;

            Transition currentTransition = _GetTransition(CurrentTransitionName, out _, true, false);
            currentTransition._Invoking(_currentParameters);
            CurrentStateName = currentTransition.StateTo;
            CurrentTransitionName = default;

            return this;
        }

        internal StateMachine<TKeyState, TKeyTransition, TKeyData> _ChangeState()
        {
            State currentState = _GetState(CurrentStateName, out bool result, true, false);
            currentState._Entry(_currentParameters, true);
            State previousState = null;
            List<object> obj = new List<object>(); 
            string message;

            if (object.Equals(PreviousStateName, default(TKeyState)))
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
            if (object.Equals(_startState, default(TKeyState)))
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

            while (_nextTransition != null)
            {
                _InvokeTransition();

                _ChangeState();
            }
            _logger.LogInformation("End work state machine");

        }

        public bool DataExists<TKeyData1>(TKeyData1 nameDate) where TKeyData1:TKeyData
        {
            throw new NotImplementedException();
        }
    }
}
