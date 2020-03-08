using System.Collections.Generic;
using System.Xml.Linq;
using System;
using SimpleStateMachineLibrary.Helpers;

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

        public StateMachine OnChangeState(Action<State, State> actionOnChangeState)
        {
            _onChangeState += actionOnChangeState;

            return this;
        }

        public StateMachine()
        {

        }

        public StateMachine(XDocument xDocument)
        {
            FromXDocument(this, xDocument);
        }

        public StateMachine(string xDocumentPath)
        {
            FromXDocument(this, xDocumentPath);
        }

        private Transition _nextTransition { get; set; }

        private Dictionary<string, object> _currentParameters { get; set; }

        private Dictionary<string, object> _nextParameters { get; set; }

        private Action<State, State> _onChangeState;

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

        public void InvokeTransition(string nameTransition)
        {
            _nextTransition = Check.GetElement(_transitions, nameTransition, true);
        }

        public void InvokeTransitionWithParameters(string nameTransition, Dictionary<string, object> parameters)
        {        
            InvokeTransition(nameTransition);

            _nextParameters = parameters;
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
