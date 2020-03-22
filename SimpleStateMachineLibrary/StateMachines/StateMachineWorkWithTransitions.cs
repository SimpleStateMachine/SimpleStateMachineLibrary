using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        //internal Transition _GetTransition(Transition transition, out bool result, bool exception)
        //{
        //    var _transition = Check.GetElement(_transitions, transition, this._logger, out result, exception);

        //    if(exception)
        //         _logger?.LogDebug("Get transition \"{NameTransition}\"", transition.Name);
        //    else
        //        _logger?.LogDebug("Try get transition \"{NameTransition}\"", transition.Name);

        //    return _transition;
        //}

        internal Transition _GetTransition(string nameTransition, out bool result, bool exception)
        {
            var _transition = Check.GetElement(_transitions, nameTransition, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get transition \"{NameTransition}\"", nameTransition);
            else
                _logger?.LogDebug("Try get transition \"{NameTransition}\"", nameTransition);

            return _transition;
        }


        public Transition GetTransition(string nameTransition)
        {
            return _GetTransition(nameTransition, out bool result, true);
        }

        public Transition TryGetTransition(string nameTransition, out bool result)
        {
            return _GetTransition(nameTransition, out result, false);
        }

        //public Transition TryGetTransition(Transition transition, out bool result)
        //{
        //    return _GetTransition(transition, out result, false);
        //}






        internal Transition _AddTransition(string nameTransition, State stateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke, out bool result, bool exception)
        {
            //throw that element already contains 
            result = Check.NotContains(_transitions, nameTransition, this._logger, exception);
             
            if (!result)
                return null;

            return new Transition(this, nameTransition, stateFrom, stateTo, actionOnInvoke);
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
     
        internal Transition AddTransition(XElement xElement)
        {
            return SimpleStateMachineLibrary.Transition.FromXElement(this, Check.Object(xElement, this._logger));
        }


        public Transition AddTransition(string nameTransition, State stateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo, actionOnInvoke, out bool result, true);
        }

        public Transition AddTransition(string nameTransition, State stateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom, GetState(nameStateTo), actionOnInvoke, out bool result, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, GetState(nameStateFrom), stateTo, actionOnInvoke, out bool result, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, GetState(nameStateFrom), GetState(nameStateTo), actionOnInvoke, out bool result, true);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, State stateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom, stateTo, actionOnInvoke, out result, false);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, State stateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom, GetState(nameStateTo), actionOnInvoke, out result, false);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, GetState(nameStateFrom), stateTo, actionOnInvoke, out result, false);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, GetState(nameStateFrom), GetState(nameStateTo), actionOnInvoke, out result, false);
        }




        internal Transition _DeleteTransition(Transition transition, out bool result, bool exception)
        {
            var _transition = Check.Remove(_transitions, transition, this._logger,out result, exception);

            if (exception)
                _logger?.LogDebug("Delete transition \"{NameTransition}\"", transition.Name);
            else
                _logger?.LogDebug("Try delete transition \"{NameTransition}\"", transition.Name);

            return _transition;
        }

        internal Transition _DeleteTransition(string transitionName, out bool result, bool exception)
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
