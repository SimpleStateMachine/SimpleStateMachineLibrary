using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        internal static XElement ToXElement(State state, bool withLog)
        {
            Check.NamedObject(state, state?.StateMachine?._logger);
            XElement element = new XElement("State");
            element.Add(new XAttribute("Name", state.Name));

            if(withLog)
                state.StateMachine._logger.LogDebug("State \"{NameState}\" to XElement", state.Name);

            return element;
        }

        internal XElement ToXElement(bool withLog)
        {
            return State.ToXElement(this, withLog);
        }

        internal static State FromXElement(StateMachine stateMachine, XElement state, bool withLog)
        {
            string Name = state.Attribute("Name")?.Value;


            State stateObj = stateMachine._AddState(Name, null, null, out bool result, true, false);

            if ((result) && (withLog))
                stateMachine?._logger.LogDebug("Initialization state \"{NameState}\" from XElement", Name);

            return stateObj;
        }


    }
}
