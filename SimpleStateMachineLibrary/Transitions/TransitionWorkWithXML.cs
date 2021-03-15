using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Transition
    {
        internal static XElement _ToXElement(Transition transition, bool withLog)
        {
            Check.NamedObject(transition, transition?.StateMachine?._logger);
            var element = new XElement("Transition");
            element.Add(new XAttribute("Name", transition.Name));
            element.Add(new XAttribute("From", transition.StateFrom));
            element.Add(new XAttribute("To", transition.StateTo));
            
            if(withLog)
                transition.StateMachine._logger.LogDebug("Transition \"{NameTransition}\" to XElement", transition.Name);

            return element;
        }

        internal XElement _ToXElement(bool withLog)
        {
            return Transition._ToXElement(this, withLog);
        }

        internal static Transition _FromXElement(StateMachine stateMachine, XElement transition, bool withLog)
        {
            stateMachine = Check.Object(stateMachine, stateMachine?._logger);
            transition = Check.Object(transition, stateMachine?._logger);

            var Name = transition.Attribute("Name")?.Value;
            var From = transition.Attribute("From")?.Value;
            var To = transition.Attribute("To")?.Value;

            var transitionObj = stateMachine._AddTransition(Name, From, To, null, out var result, true, false);
            if((result)&&(withLog))
                stateMachine?._logger.LogDebug("Initialization transition \"{NameTransition}\" from XElement", Name);

            return transitionObj;
        }
    }
}
