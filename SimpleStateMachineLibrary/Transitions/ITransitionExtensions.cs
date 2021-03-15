using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public static class ITransitionExtensions
    {
        public static InvokeParameters Invoke(this ITransition transition, Dictionary<string, object> parameters)
        {
            // transition.
            return null;
            // return StateMachine.InvokeTransition(this, parameters);
        }
        public static ITransition Delete(this ITransition transition)
        {
            return null;
            // return this.StateMachine.DeleteTransition(this);
        }
        public static ITransition TryDelete(this ITransition transition, out bool result)
        {
            result = false;
            return null;
            // return this.StateMachine.TryDeleteTransition(this, out result);
        }
    }
}