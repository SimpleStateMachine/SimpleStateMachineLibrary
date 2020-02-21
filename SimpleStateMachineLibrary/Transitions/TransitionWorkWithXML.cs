using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Transition
    {
        public static XElement ToXElement(Transition transition)
        {
            Check.NamedObject(transition);
            XElement element = new XElement("Transition");
            element.Add(new XAttribute("Name", transition.Name));
            element.Add(new XAttribute("From", transition.StateFrom.Name));
            element.Add(new XAttribute("To", transition.StateTo.Name));
            return element;
        }

        public XElement ToXElement()
        {
            XElement element = new XElement("Transition");
            element.Add(new XAttribute("Name", this.Name));
            element.Add(new XAttribute("From", this.StateFrom.Name));
            element.Add(new XAttribute("To", this.StateTo.Name));
            return element;
        }

        public static Transition FromXElement(StateMachine stateMachine, XElement transition)
        {
            stateMachine = Check.Object(stateMachine);
            transition = Check.Object(transition);

            string Name = transition.Attribute("Name")?.Value;
            string From = transition.Attribute("From")?.Value;
            string To = transition.Attribute("To")?.Value;
            return stateMachine.AddTransition(Name, From, To);
        }
    }
}
