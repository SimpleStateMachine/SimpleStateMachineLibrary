using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class State
    {
        public static XElement ToXElement(State state)
        {
            Check.NamedObject(state);
            XElement element = new XElement("State");
            element.Add(new XAttribute("Name", state.Name));

            state.StateMachine._logger?.LogDebug("State \"{NameState}\" to XElement", state.Name);
            return element;
        }

        public XElement ToXElement()
        {
            return State.ToXElement(this);
        }

        public static State FromXElement(StateMachine stateMachine, XElement state)
        {
            string Name = state.Attribute("Name")?.Value;

            stateMachine._logger?.LogDebug("Initialization state \"{NameState}\" from XElement", Name);
            return stateMachine.AddState(Name);
        }


    }
}
