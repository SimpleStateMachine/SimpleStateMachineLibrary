using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        private Transition _Transition(Transition transition, out bool result, bool exception)
        {
            var _transition = Check.GetElement(_transitions, transition, this._logger, out result, exception);

            if(exception)
                 _logger?.LogDebug("Get transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try get transition \"{NameTransition}\"", transition.Name);

            return _transition;
        }

        private Transition _Transition(string nameTransition, out bool result, bool exception)
        {
            var _transition = Check.GetElement(_transitions, nameTransition, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get transition \"{NameTransition}\"", nameTransition);
            else
                _logger?.LogDebug("Try get transition \"{NameTransition}\"", nameTransition);

            return _transition;
        }

        public Transition Transition(string nameTransition)
        {
            return _Transition(nameTransition, out bool result, true);
        }

        public Transition TryGetTransition(Transition transition, out bool result)
        {
            return _Transition(transition, out result, false);
        }

        public Transition TryGetTransition(string nameTransition, out bool result)
        {
            return _Transition(nameTransition, out result, false);
        }


        private Transition _AddTransition(string nameTransition, State stateFrom, State stateTo, out bool result, bool exception)
        {
            //throw that element already contains 
            result = Check.NotContains(_transitions, nameTransition, this._logger, exception);
             
            if (!result)
                return null;

            return new Transition(this, nameTransition, stateFrom, stateTo);
        }

        internal Transition AddTransition(Transition transition, out bool result, bool exception)
        {
            //throw that element already contains 
            result = Check.NotContains(_transitions, transition, this._logger, exception);
             
            if (!result)
                return null;

            _transitions.Add(transition.Name, transition);

            if (exception)
                _logger?.LogDebug("Add transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try add transition \"{NameTransition}\"", transition.Name);

            return transition;
        }

        public Transition AddTransition(string nameTransition, State stateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo, out bool result, true);
        }

        public Transition AddTransition(string nameTransition, State stateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, stateFrom, State(nameStateTo), out bool result, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, State stateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), stateTo, out bool result, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, string nameStateTo)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), State(nameStateTo), out bool result, true);
        }

        public Transition AddTransition(XElement xElement)
        {
            return SimpleStateMachineLibrary.Transition.FromXElement(this, Check.Object(xElement, this._logger));
        }

        public Transition TryAddTransition(string nameTransition, State stateFrom, State stateTo, out bool result)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo,out result, false);
        }

        public Transition TryAddTransition(string nameTransition, State stateFrom, string nameStateTo, out bool result)
        {
            return _AddTransition(nameTransition, stateFrom, State(nameStateTo), out result, false);
        }

        public Transition TryAddTransition(string nameTransition, string nameStateFrom, State stateTo, out bool result)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), stateTo, out result, false);
        }

        public Transition TryAddTransition(string nameTransition, string nameStateFrom, string nameStateTo, out bool result)
        {
            return _AddTransition(nameTransition, State(nameStateFrom), State(nameStateTo), out result, false);
        }


        private Transition _DeleteTransition(Transition transition, out bool result, bool exception)
        {
            var _transition = Check.Remove(_transitions, transition, this._logger,out result, exception);

            if (exception)
                _logger?.LogDebug("Delete transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try delete transition \"{NameTransition}\"", transition.Name);

            return _transition;
        }

        private Transition _DeleteTransition(string transitionName, out bool result, bool exception)
        {
            var _transition = Check.Remove(_transitions, transitionName, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Delete transition \"{NameTransition}\"", transitionName);
            else
                _logger?.LogDebug("Try delete transition \"{NameTransition}\"", transitionName);

            return _transition;
        }

        public Transition DeleteTransition(Transition transition)
        {
            return _DeleteTransition(transition, out bool result, true);
        }

        public Transition DeleteTransition(string transitionName)
        {
            return _DeleteTransition(transitionName, out bool result, true);
        }

        public Transition TryDeleteTransition(Transition transition, out bool result)
        {
            return _DeleteTransition(transition, out result, false);
        }

        public Transition TryDeleteTransition(string transitionName, out bool result)
        {
            return _DeleteTransition(transitionName, out result, false);
        }
    }
}
