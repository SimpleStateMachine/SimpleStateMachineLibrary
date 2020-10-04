using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachineLibrary
{
    public class InvokeParameters<TKeyState, TKeyTransition, TKeyData>
    {
        internal InvokeParameters(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine)
        {
            StateMachine = stateMachine;
        }

        internal StateMachine<TKeyState, TKeyTransition, TKeyData> StateMachine;
    }

    public static class InvokeParametersExtension
    {
        public static InvokeParameters<TKeyState, TKeyTransition, TKeyData> AddParameter<TKeyState, TKeyTransition, TKeyData>(this InvokeParameters<TKeyState, TKeyTransition, TKeyData> invokeParameters, string nameParameter, object valueParameter)
        {

            if(invokeParameters.StateMachine._nextParameters ==null)
            {
                invokeParameters.StateMachine._nextParameters = new Dictionary<string, object>();
            }

            invokeParameters.StateMachine._nextParameters.Add(nameParameter, valueParameter);
            return invokeParameters;
        }

        public static InvokeParameters<TKeyState, TKeyTransition, TKeyData> AddParameters<TKeyState, TKeyTransition, TKeyData>(this InvokeParameters<TKeyState, TKeyTransition, TKeyData> invokeParameters, Dictionary<string, object> parameters)
        {

            if (invokeParameters.StateMachine._nextParameters == null)
            {
                invokeParameters.StateMachine._nextParameters = new Dictionary<string, object>();
            }

            foreach (var parameter in parameters)
            {
                invokeParameters.StateMachine._nextParameters.Add(parameter.Key, parameter.Value);
            }

            return invokeParameters;
        }
    }
}
