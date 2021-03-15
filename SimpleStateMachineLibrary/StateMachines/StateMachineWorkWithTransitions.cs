using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        public bool TransitionExists(string nameTransition)
        {
            nameTransition = _TransitionExists(nameTransition,out var result, false, true);
            return result;
        }
        public ITransition GetTransition(string nameTransition)
        {
            return _GetTransition(nameTransition, out var result, true, true);
        }
        public ITransition TryGetTransition(string nameTransition, out bool result)
        {
            return _GetTransition(nameTransition, out result, false, true);
        }
        
        public ITransition AddTransition(string nameTransition, IState stateFrom, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, stateTo?.Name, actionOnInvoke, out var result, true, true);
        }
        public ITransition AddTransition(string nameTransition, IState stateFrom, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, nameStateTo, actionOnInvoke, out var result, true, true);
        }
        public ITransition AddTransition(string nameTransition, string nameStateFrom, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, stateTo?.Name, actionOnInvoke, out var result, true, true);
        }
        public ITransition AddTransition(string nameTransition, string nameStateFrom, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, nameStateTo, actionOnInvoke, out var result, true, true);
        }
        public ITransition TryAddTransition(out bool result, string nameTransition, IState stateFrom, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, stateTo?.Name, actionOnInvoke, out result, false, true);
        }
        public ITransition TryAddTransition(out bool result, string nameTransition, IState stateFrom, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, stateFrom?.Name, nameStateTo, actionOnInvoke, out result, false, true);
        }
        public ITransition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, stateTo?.Name, actionOnInvoke, out result, false, true);
        }
        public ITransition TryAddTransition(out bool result, string nameTransition, string nameStateFrom, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return _AddTransition(nameTransition, nameStateFrom, nameStateTo, actionOnInvoke, out result, false, true);
        }
        
        public ITransition DeleteTransition(ITransition transition)
        {
            return _DeleteTransition(transition, out _, true, true);
        }
        public ITransition DeleteTransition(string transitionName)
        {
            return _DeleteTransition(transitionName, out _, true, true);
        }
        public ITransition TryDeleteTransition(ITransition transition, out bool result)
        {
            return _DeleteTransition(transition, out result, false, true);
        }
        public ITransition TryDeleteTransition(string transitionName, out bool result)
        {
            return _DeleteTransition(transitionName, out result, false, true);
        }
        
        
        internal string _TransitionExists(string nameTransition, out bool result,  bool exeption, bool withLog)
        {
            return Check.Contains(Transitions, nameTransition, this._logger, out result, exeption);
        }
        
        internal ITransition _GetTransition(string nameTransition, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.GetElement(Transitions, nameTransition, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get transition \"{NameTransition}\"", nameTransition);
                else
                    _logger.LogDebug("Try get transition \"{NameTransition}\"", nameTransition);
            }

            return _transition;
        }
        
        internal ITransition _AddTransition(string nameTransition, string stateFrom, string stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(Transitions, nameTransition, this._logger, exception);
             
            if (!result)
                return null;

            return new ITransition(this, nameTransition, stateFrom, stateTo, actionOnInvoke, withLog);
        }
        internal ITransition _AddTransition(ITransition transition, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(Transitions, transition, this._logger, exception);
             
            if (!result)
                return null;

            Transitions.Add(transition.Name, transition);
            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", transition.Name, transition.StateFrom, transition.StateTo);
                else
                    _logger.LogDebug("Try add transition \"{NameTransition}\" from state \"{NameStateFrom}\" to state \"{NameStateTo}\"", transition.Name, transition.StateFrom, transition.StateTo);
            }

            return transition;
        }
        internal ITransition _AddTransition(XElement xElement, bool withLog)
        {
            return ITransition._FromXElement(this, Check.Object(xElement, this._logger), true);
        }

        
        internal ITransition _DeleteTransition(ITransition transition, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.Remove(Transitions, transition, this._logger,out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete transition \"{NameTransition}\"", transition.Name);
                else
                    _logger.LogDebug("Try delete transition \"{NameTransition}\"", transition.Name);
            }

            return _transition;
        }
        internal ITransition _DeleteTransition(string transitionName, out bool result, bool exception, bool withLog)
        {
            var _transition = Check.Remove(Transitions, transitionName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete transition \"{NameTransition}\"", transitionName);
                else
                    _logger.LogDebug("Try delete transition \"{NameTransition}\"", transitionName);
            }

            return _transition;
        }
    }
}
