using System.Collections.Generic;
using System.Xml.Linq;

using SimpleStateMachineLibrary.States;
using SimpleStateMachineLibrary.Transitions;
using SimpleStateMachineLibrary.Helpers;
using SimpleStateMachineLibrary.Datas;
using System;

namespace SimpleStateMachineLibrary.StateMachines
{
    public partial class StateMachine
    {
        private Dictionary<string, State> _states = new Dictionary<string, State>();

        private Dictionary<string, Transition> _transitions = new Dictionary<string, Transition>();

        private Dictionary<string, Data> _data = new Dictionary<string, Data>();

        public State CurrentState { get; internal set; }

        public Transition CurrentTransition{ get; internal set; }

        public State StartState { get; protected set; }

        public State EndState { get; protected set; }

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



        private void CheckStartState()
        {
            if (StartState != null) 
            {
                throw new ArgumentException(String.Format("Start state already set. It's {0} ", StartState.Name));
            }
        }

        private void CheckEndState()
        {
            if (EndState != null)
            {
                throw new ArgumentException(String.Format("End state already set. It's {0} ", StartState.Name));
            }
        }

        private void CheckCurrentTransition()
        {
            if (CurrentTransition == null)
            {
                throw new ArgumentException(String.Format("State with name \"{0}\" doesn't invoke transition", CurrentState.Name));
            }
        }

        public State SetStartState(State state)
        {
            CheckStartState();
            StartState = State(Check.Object(state));
            return StartState;
        }

        public State SetStartState(string stateName)
        {
            CheckEndState();
            StartState = State(Check.Object(stateName));
            return StartState;
        }

        public State SetEndState(State state)
        {
            EndState = State(Check.Object(state));
            return EndState;
        }

        public State SetEndState(string stateName)
        {
            EndState = State(Check.Object(stateName));
            return EndState;
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

        public void Start(Dictionary<string, object> startParameters = null)
        {
            CurrentState = StartState;
            CurrentState.Entry(startParameters);
            CurrentState.Exit(startParameters);
            while (CurrentState != EndState)
            {               
                _currentParameters = _nextParameters;
                _nextParameters = null;

                CurrentTransition = _nextTransition;
                _nextTransition = null;

                CheckCurrentTransition();
                CurrentState = null;
                CurrentTransition.Invoke(_currentParameters);
                CurrentState = CurrentTransition.StateTo;
                CurrentTransition = null;

                CurrentState.Entry(_currentParameters);
                CurrentState.Exit(_currentParameters);                
            }
        
        }

    }
}
