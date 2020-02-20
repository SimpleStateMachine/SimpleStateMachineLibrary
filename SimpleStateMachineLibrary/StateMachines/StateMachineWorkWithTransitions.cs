
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

        public Transition Transition(Transition transition)
        {
            return _Transition(transition, true);
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
            Transition newTransition = new Transition(this, nameTrancition, stateFrom, stateTo);
            _transitions.Add(nameTrancition, newTransition);
            return newTransition;
        }

        private Transition _AddTransition(Transition transition, bool exeption)
        {
            return Check.AddElement(_transitions, transition, exeption);
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

        public Transition AddTransition(Transition transition)
        {
            return _AddTransition(transition, true);
        }

        public Transition AddTransition(XElement xElement)
        {
            return SimpleStateMachineLibrary.Transition.FromXElement(this, Check.Object(xElement));
        }

        public Transition TryAddTransition(string nameTrancition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, State(stateFrom), State(stateTo), false);
        }

        public Transition TryAddTransition(string nameTrancition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, State(stateFrom), State(nameStateTo), false);
        }

        public Transition TryAddTransition(string nameTrancition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), State(stateTo), false);
        }

        public Transition TryAddTransition(string nameTrancition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTrancition, State(nameStateFrom), State(nameStateTo), false);
        }

        public Transition TryAddTransition(Transition transition)
        {
            return _AddTransition(transition, false);
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
            return _DeleteTransition(Transition(transition), true);
        }

        public Transition DeleteTransition(string transitionName)
        {
            return _DeleteTransition(Transition(transitionName), true);
        }

        public Transition TryDeleteTransition(Transition transition)
        {
            return _DeleteTransition(Transition(transition), false);
        }

        public Transition TryDeleteTransition(string transitionName)
        {
            return _DeleteTransition(Transition(transitionName), false);
        }
    }
}
