
using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private Transition _Transition(Transition transition, bool exeption)
        {
            var _transition = Check.GetElement(_transitions, transition, exeption);

            if(exeption)
                 _logger?.LogDebug("Get transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try get transition \"{NameTransition}\"", transition.Name);

            return _transition;
        }

        private Transition _Transition(string nameTransition, bool exeption)
        {
            var _transition = Check.GetElement(_transitions, nameTransition, exeption);

            if (exeption)
                _logger?.LogDebug("Get transition \"{NameTransition}\"", nameTransition);
            else
                _logger?.LogDebug("Try get transition \"{NameTransition}\"", nameTransition);

            return _transition;
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


        private Transition _AddTransition(string nameTransition, State stateFrom, State stateTo, bool exeption)
        {
            if (!Check.NotContains(_transitions, nameTransition, exeption))
                return null;

            return new Transition(this, nameTransition, stateFrom, stateTo);
        }

        internal Transition AddTransition(Transition transition, bool exeption)
        {
            if (!Check.NotContains(_transitions, transition, exeption))
                return null;

            _transitions.Add(transition.Name, transition);

            if (exeption)
                _logger?.LogDebug("Add transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try add transition \"{NameTransition}\"", transition.Name);

            return transition;
        }

        public Transition AddTransition(string nameTransition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo, true);
        }

        public Transition AddTransition(string nameTransition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, stateFrom, State(nameStateTo), true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), stateTo, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), State(nameStateTo), true);
        }

        public Transition AddTransition(XElement xElement)
        {
            return SimpleStateMachineLibrary.Transition.FromXElement(this, Check.Object(xElement));
        }

        public Transition TryAddTransition(string nameTransition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo, false);
        }

        public Transition TryAddTransition(string nameTransition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, stateFrom, State(nameStateTo), false);
        }

        public Transition TryAddTransition(string nameTransition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), stateTo, false);
        }

        public Transition TryAddTransition(string nameTransition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), State(nameStateTo), false);
        }


        private Transition _DeleteTransition(Transition transition, bool exeption)
        {
            var _transition = Check.Remove(_transitions, transition, exeption);

            if (exeption)
                _logger?.LogDebug("Delete transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try delete transition \"{NameTransition}\"", transition.Name);

            return _transition;
        }

        private Transition _DeleteTransition(string transitionName, bool exeption)
        {
            var _transition = Check.Remove(_transitions, transitionName, exeption);

            if (exeption)
                _logger?.LogDebug("Delete transition \"{NameTransition}\"", transitionName);
            else
                _logger?.LogDebug("Try delete transition \"{NameTransition}\"", transitionName);

            return _transition;
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
