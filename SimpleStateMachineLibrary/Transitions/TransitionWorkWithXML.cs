﻿using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Transition
    {
        internal static XElement ToXElement(Transition transition)
        {
            Check.NamedObject(transition, transition?.StateMachine?._logger);
            XElement element = new XElement("Transition");
            element.Add(new XAttribute("Name", transition.Name));
            element.Add(new XAttribute("From", transition.StateFrom));
            element.Add(new XAttribute("To", transition.StateTo));

            transition.StateMachine._logger?.LogDebug("Transition \"{NameTransition}\" to XElement", transition.Name);
            return element;
        }

        internal XElement ToXElement()
        {
            return Transition.ToXElement(this);
        }

        internal static Transition FromXElement(StateMachine stateMachine, XElement transition)
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
