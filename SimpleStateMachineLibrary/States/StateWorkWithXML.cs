using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        internal static XElement ToXElement(State state)
        {
            Check.NamedObject(state, state?.StateMachine?._logger);
            XElement element = new XElement("State");
            element.Add(new XAttribute("Name", state.Name));

            state.StateMachine._logger?.LogDebug("State \"{NameState}\" to XElement", state.Name);
            return element;
        }

        internal XElement ToXElement()
        {
            return State.ToXElement(this);
        }

        internal static State FromXElement(StateMachine stateMachine, XElement state)
        {
            string Name = state.Attribute("Name")?.Value;

            stateMachine?._logger?.LogDebug("Initialization state \"{NameState}\" from XElement", Name);
            return stateMachine.AddState(Name);
        }


    }
}
