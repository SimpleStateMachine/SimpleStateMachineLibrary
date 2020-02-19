using SimpleStateMachineLibrary.States;
using SimpleStateMachineLibrary.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStateMachineLibrary.StateMachines
{
    public partial class StateMachine
    {
        public List<Transition> GetTransitionsFromState(string stateName)
        {
            State state = State(stateName, true);
            return _transitions.Values.Where(x => x.StateFrom.Name == state.Name).ToList();
        }

        public List<Transition> GetTransitionsFromState(State state)
        {
            state = State(state, true);
            return _transitions.Values.Where(x => x.StateFrom.Name == state.Name).ToList();
        }

        public List<Transition> GetTransitionsToState(State state)
        {
            state = State(state, true);
            return _transitions.Values.Where(x => x.StateTo.Name == state.Name).ToList();
        }

        public List<Transition> GetTransitionsToState(string stateName)
        {
            State state = State(stateName, true);
            return _transitions.Values.Where(x => x.StateTo.Name == state.Name).ToList();
        }
    }
}
