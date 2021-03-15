using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public static class IStateExtensions
    {
        public static IState Delete(this IState state)
        {
            return null;
            // return this.StateMachine.DeleteState(this);
        }
        public static IState TryDelete(this IState state, out bool result)
        {
            result = false;
            return null;
            // return this.StateMachine.TryDeleteState(this, out result);
        }
        public static IState SetAsStartState(this IState state)
        {
            return null;
            // this.StateMachine.SetStartState(this);
            // return this;
        }
        
        #region Transitions from this state
        
        public static Dictionary<string, ITransition> GetTransitionsFromThis(this IState state)
        {
            return null;
            // return this.StateMachine.GetTransitionsFromState(this);
        }
        public static Dictionary<string, ITransition> TryGetTransitionsFromThis(this IState state, out bool result)
        {
            result = false;
            return null;
            // return this.StateMachine.TryGetTransitionsFromState(this, out result);
        }
        public static ITransition AddTransitionFromThis(this IState state, string nameTransition, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return null;
            // return this.StateMachine.AddTransition(nameTransition, this, stateTo, actionOnInvoke);
        }
        public static ITransition AddTransitionFromThis(this IState state, string nameTransition, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return null;
            // return this.StateMachine.AddTransition(nameTransition, this, nameStateTo, actionOnInvoke);
        }
        public static ITransition TryAddTransitionFromThis(this IState state, out bool result, string nameTransition, IState stateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            result = false;
            return null;
            // return this.StateMachine.TryAddTransition(out result, nameTransition, this, stateTo, actionOnInvoke);
        }
        public static ITransition TryAddTransitionFromThis(this IState state, out bool result, string nameTransition, string nameStateTo, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            result = false;
            return null;
            // return this.StateMachine.TryAddTransition(out result, nameTransition, this, nameStateTo, actionOnInvoke);
        }
        
        #endregion Transitions from this state
        
        #region Transitions to this state
        public static Dictionary<string, ITransition> GetTransitionsToThis(this IState state)
        {
            return null;
            // return this.StateMachine.GetTransitionsToState(this);
        }
        public static Dictionary<string, ITransition> TryGetTransitionsToThis(this IState state, out bool result)
        {
            result = false;
            return null;
            // return this.StateMachine.TryGetTransitionsToState(this, out result);
        }
        public static ITransition AddTransitionToThis(this IState state, string nameTransition, IState stateFrom, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return null;
            // return this.StateMachine.AddTransition(nameTransition, stateFrom, this);
        }
        public static ITransition AddTransitionToThis(this IState state, string nameTransition, string nameStateFrom, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            return null;
            // return this.StateMachine.AddTransition(nameTransition, nameStateFrom, this);
        }
        public static ITransition TryAddTransitionToThis(this IState state, out bool result, string nameTransition, IState stateFrom, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            result = false;
            return null;
            // return this.StateMachine.TryAddTransition(out result, nameTransition, stateFrom, this, actionOnInvoke);
        }
        public static ITransition TryAddTransitionToThis(this IState state, out bool result, string nameTransition, string nameStateFrom, Action<ITransition, Dictionary<string, object>> actionOnInvoke = null)
        {
            result = false;
            return null;
            // return this.StateMachine.TryAddTransition(out result, nameTransition, nameStateFrom, this, actionOnInvoke);
        }
        
        #endregion Transitions to this state
    }
}