using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Transition
    {
        public static XElement ToXElement(Transition transition)
        {
            Check.NamedObject(transition, transition?.StateMachine?._logger);
            XElement element = new XElement("Transition");
            element.Add(new XAttribute("Name", transition.Name));
            element.Add(new XAttribute("From", transition.StateFrom.Name));
            element.Add(new XAttribute("To", transition.StateTo.Name));

            transition.StateMachine._logger?.LogDebug("Transition \"{NameTransition}\" to XElement", transition.Name);
            return element;
        }

        public XElement ToXElement()
        {
            return Transition.ToXElement(this);
        }

        public static Transition FromXElement(StateMachine stateMachine, XElement transition)
        {
            stateMachine = Check.Object(stateMachine, stateMachine?._logger);
            transition = Check.Object(transition, stateMachine?._logger);

            string Name = transition.Attribute("Name")?.Value;
            string From = transition.Attribute("From")?.Value;
            string To = transition.Attribute("To")?.Value;

            stateMachine?._logger?.LogDebug("Initialization transition \"{NameTransition}\" from XElement", Name);
            return stateMachine.AddTransition(Name, From, To);
        }
    }
}
