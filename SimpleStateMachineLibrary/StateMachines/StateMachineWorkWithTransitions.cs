﻿using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        internal string _TransitionExists(string nameTransition, out bool result,  bool exeption, bool withLog)
        {
            return Check.Contains(_transitions, nameTransition, this._logger, out result, exeption);
        }

        public bool TransitionExists(string nameTransition)
        {
            nameTransition = _TransitionExists(nameTransition,out bool result, false, true);
            return result;
        }

        internal Transition _GetTransition(string nameTransition, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.GetElement(_transitions, nameTransition, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get transition \"{NameTransition}\"", nameTransition);
                else
                    _logger.LogDebug("Try get transition \"{NameTransition}\"", nameTransition);
            }

            return _transition;
        }

        public Transition GetTransition(string nameTransition)
        {
            return _GetTransition(nameTransition, out bool result, true, true);
        }

        public Transition TryGetTransition(string nameTransition, out bool result)
        {
            return _GetTransition(nameTransition, out result, false, true);
        }


        internal Transition _AddTransition(string nameTransition, string stateFrom, string stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(_transitions, nameTransition, this._logger, exception);
             
            if (!result)
                return null;

            return new Transition(this, nameTransition, stateFrom, stateTo, actionOnInvoke, withLog);
        }

        internal Transition _AddTransition(Transition transition, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(_transitions, transition, this._logger, exception);
             
            if (!result)
                return null;

            _transitions.Add(transition.Name, transition);
            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", transition.Name, transition.StateFrom, transition.StateTo);
                else
                    _logger.LogDebug("Try add transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", transition.Name, transition.StateFrom, transition.StateTo);
            }

            return transition;
        }
     
        internal Transition _AddTransition(XElement xElement, bool withLog)
        {
            return Transition._FromXElement(this, Check.Object(xElement, this._logger), true);
        }


        public Transition AddTransition(string nameTransition, State stateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, stateTo?.Name, actionOnInvoke, out bool result, true, true);
        }

        public Transition AddTransition(string nameTransition, State stateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, nameStateTo, actionOnInvoke, out bool result, true, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, stateTo?.Name, actionOnInvoke, out bool result, true, true);
        }

        public Transition AddTransition(string nameTransition, string nameStateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, nameStateTo, actionOnInvoke, out bool result, true, true);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, State stateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, stateTo?.Name, actionOnInvoke, out result, false, true);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, State stateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, nameStateTo, actionOnInvoke, out result, false, true);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, State stateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, stateTo?.Name, actionOnInvoke, out result, false, true);
        }

        public Transition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, string nameStateTo, Action<Transition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, nameStateTo, actionOnInvoke, out result, false, true);
        }




        internal Transition _DeleteTransition(Transition transition, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.Remove(_transitions, transition, this._logger,out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete transition \"{NameTransition}\"", transition.Name);
                else
                    _logger.LogDebug("Try delete transition \"{NameTransition}\"", transition.Name);
            }

            return _transition;
        }

        internal Transition _DeleteTransition(string transitionName, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.Remove(_transitions, transitionName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete transition \"{NameTransition}\"", transitionName);
                else
                    _logger.LogDebug("Try delete transition \"{NameTransition}\"", transitionName);
            }

            return _transition;
        }


        public Transition DeleteTransition(Transition transition)
        {
            return _DeleteTransition(transition, out bool result, true, true);
        }

        public Transition DeleteTransition(string transitionName)
        {
            return _DeleteTransition(transitionName, out bool result, true, true);
        }

        public Transition TryDeleteTransition(Transition transition, out bool result)
        {
            return _DeleteTransition(transition, out result, false, true);
        }

        public Transition TryDeleteTransition(string transitionName, out bool result)
        {
            return _DeleteTransition(transitionName, out result, false, true);
        }
    }
}
