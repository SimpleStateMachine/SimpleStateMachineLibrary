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
            return element;
        }

        public static State FromXElement(StateMachine stateMachine, XElement state)
        {
            string Name = state.Attribute("Name")?.Value;
            return stateMachine.AddState(Name);
        }

        public XElement ToXElement()
        {
            XElement element = new XElement("State");
            element.Add(new XAttribute("Name", this.Name));
            return element;
        }
    }
}
