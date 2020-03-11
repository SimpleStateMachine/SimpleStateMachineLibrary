using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachineLibrary
{
    public class InvokeParameters
    {
        internal InvokeParameters(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        internal StateMachine StateMachine;
    }

    public static class InvokeParametersExtension
    {
        public static InvokeParameters AddParameter(this InvokeParameters invokeParameters, string nameParameter, object valueParameter)
        {
            invokeParameters.StateMachine._nextParameters.Add(nameParameter, valueParameter);
            return invokeParameters;
        }

        public static InvokeParameters AddParameters(this InvokeParameters invokeParameters, Dictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                invokeParameters.StateMachine._nextParameters.Add(parameter.Key, parameter.Value);
            }

            return invokeParameters;
        }
    }
}
