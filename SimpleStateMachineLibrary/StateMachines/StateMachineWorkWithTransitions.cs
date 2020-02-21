
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private Transition _Transition(Transition transition, bool exeption)
        {
            return Check.GetElement(_transitions, transition, exeption);
        }

        private Transition _Transition(string nameTransition, bool exeption)
        {
            return Check.GetElement(_transitions, nameTransition, exeption);
        }

        public Transition Transition(string nameTransition)
        {
            return _Transition(nameTransition, true);
        }

        public Transition TryGetTransition(Transition transition)
        {
            return _Transition(transition, false);
        }

        public Transition TryGetTransition(string nameTransition)
        {
            return _Transition(nameTransition, false);
        }


        private Transition _AddTransition(string nameTrancition, State stateFrom, State stateTo, bool exeption)
        {
            if (!Check.NotContains(_transitions, nameTrancition, exeption))
                return null;

            return new Transition(this, nameTrancition, stateFrom, stateTo);
        }

        public Transition AddTransition(string nameTrancition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, stateFrom, stateTo, true);
        }

        public Transition AddTransition(string nameTrancition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, stateFrom, State(nameStateTo), true);
        }

        public Transition AddTransition(string nameTrancition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), stateTo, true);
        }

        public Transition AddTransition(string nameTrancition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), State(nameStateTo), true);
        }

        public Transition AddTransition(XElement xElement)
        {
            return SimpleStateMachineLibrary.Transition.FromXElement(this, Check.Object(xElement));
        }

        public Transition TryAddTransition(string nameTrancition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, stateFrom, stateTo, false);
        }

        public Transition TryAddTransition(string nameTrancition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, stateFrom, State(nameStateTo), false);
        }

        public Transition TryAddTransition(string nameTrancition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), stateTo, false);
        }

        public Transition TryAddTransition(string nameTrancition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), State(nameStateTo), false);
        }


        private Transition _DeleteTransition(Transition transition, bool exeption)
        {
            return Check.Remove(_transitions, transition, exeption);
        }

        private Transition _DeleteTransition(string transitionName, bool exeption)
        {
            return Check.Remove(_transitions, transitionName, exeption);
        }

        public Transition DeleteTransition(Transition transition)
        {
            return _DeleteTransition(transition, true);
        }

        public Transition DeleteTransition(string transitionName)
        {
            return _DeleteTransition(transitionName, true);
        }

        public Transition TryDeleteTransition(Transition transition)
        {
            return _DeleteTransition(transition, false);
        }

        public Transition TryDeleteTransition(string transitionName)
        {
            return _DeleteTransition(transitionName, false);
        }
    }
}
