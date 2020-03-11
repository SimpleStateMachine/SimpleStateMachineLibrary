using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Linq;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        public static XDocument ToXDocument(StateMachine stateMachine, string nameFile)
        {
            Check.Object(stateMachine);
            Check.Name(nameFile);
            XDocument xDocument = new XDocument();  
            XElement stateMachineXElement = new XElement("StateMachine");
            xDocument.Add(stateMachineXElement);
            XElement states = new XElement("States");
            stateMachineXElement.Add(states);
            foreach(var state in stateMachine._states)
            {
                states.Add(state.Value.ToXElement());
            }

            if (stateMachine?.StartState != null)
            {
                XElement startState = new XElement("StartState");
                stateMachineXElement.Add(startState);
                startState.Add(new XAttribute("Name", stateMachine.StartState.Name));
            }

            XElement transitions = new XElement("Transitions");
            stateMachineXElement.Add(transitions);

            foreach (var transition in stateMachine._transitions)
            {
                transitions.Add(transition.Value.ToXElement());
            }

            XElement datas = new XElement("Data");
            stateMachineXElement.Add(datas);

            foreach (var data in stateMachine._data)
            {
                datas.Add(data.Value.ToXElement());
            }

            xDocument.Save(nameFile);
            return xDocument;
        }

        public XDocument ToXDocument(string nameFile)
        {
            return StateMachine.ToXDocument(this, nameFile);
        }

        public static StateMachine FromXDocument(StateMachine stateMachine, XDocument xDocument)
        {
            XElement stateMachineXElement = Check.Object(xDocument).Element("StateMachine");
            stateMachineXElement = Check.Object(stateMachineXElement);
            var States = stateMachineXElement.Element("States")?.Elements()?.ToList();
            States?.ForEach(x => stateMachine.AddState(x));
            var startState = stateMachineXElement.Element("StartState");
            string nameStartState = startState?.Attribute("Name").Value;
            if (!string.IsNullOrEmpty(nameStartState))
                stateMachine.SetStartState(nameStartState);

            var Transitions = stateMachineXElement.Element("Transitions")?.Elements()?.ToList();
            Transitions?.ForEach(x => stateMachine.AddTransition(x));

            var Datas = stateMachineXElement.Element("Data")?.Elements()?.ToList();
            Datas?.ForEach(x => stateMachine.AddData(x));

            return stateMachine;
        }

        public static StateMachine FromXDocument(StateMachine stateMachine, string xDocumentPath)
        {
            xDocumentPath = Check.Name(xDocumentPath);
            XDocument xDocument = XDocument.Load(xDocumentPath);
            return FromXDocument(stateMachine, xDocument);
        }

        public static StateMachine FromXDocument(XDocument xDocument, ILogger logger = null)
        {
            StateMachine stateMachine = new StateMachine(logger);
            return FromXDocument(stateMachine, xDocument);
        }

        public static StateMachine FromXDocument(string xDocumentPath, ILogger logger = null)
        {
            StateMachine stateMachine = new StateMachine(logger);
            return FromXDocument(stateMachine, xDocumentPath);
        }

    }
}
