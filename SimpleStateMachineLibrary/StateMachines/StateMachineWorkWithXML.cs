using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Linq;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        internal static XDocument _ToXDocument(StateMachine stateMachine, string nameFile, bool withLog)
        {          
            Check.Object(stateMachine, stateMachine?._logger);
            Check.Name(nameFile, stateMachine?._logger);
            var xDocument = new XDocument();  
            var stateMachineXElement = new XElement("StateMachine");
            xDocument.Add(stateMachineXElement);
            stateMachine?._logger.LogDebug("StateMachine to XDocument");
            var states = new XElement("States");
            stateMachineXElement.Add(states);
            foreach(var state in stateMachine.States)
            {
                states.Add(state.Value.ToXElement(withLog));
            }

            if (stateMachine?._startState != null)
            {
                var startState = new XElement("StartState");
                stateMachineXElement.Add(startState);
                startState.Add(new XAttribute("Name", stateMachine._startState));
            }

            var transitions = new XElement("Transitions");
            stateMachineXElement.Add(transitions);

            foreach (var transition in stateMachine.Transitions)
            {
                transitions.Add(transition.Value._ToXElement(withLog));
            }

            var datas = new XElement("DATA");
            stateMachineXElement.Add(datas);

            foreach (var data in stateMachine.Data)
            {
                datas.Add(data.Value._ToXElement(withLog));
            }

            xDocument.Save(nameFile);
       
            return xDocument;
        }

        public XDocument ToXDocument(string nameFile)
        {
            return StateMachine._ToXDocument(this, nameFile, true);
        }

        internal static StateMachine _FromXDocument(StateMachine stateMachine, XDocument xDocument, bool withLog)
        {
            var stateMachineXElement = Check.Object(xDocument, stateMachine?._logger).Element("StateMachine");
            stateMachineXElement = Check.Object(stateMachineXElement, stateMachine?._logger);
            var States = stateMachineXElement.Element("States")?.Elements()?.ToList();
            States?.ForEach(x => stateMachine._AddState(x, true));
            var startState = stateMachineXElement.Element("StartState");
            var nameStartState = startState?.Attribute("Name").Value;
            if (!string.IsNullOrEmpty(nameStartState))
                stateMachine.SetStartState(nameStartState);

            var Transitions = stateMachineXElement.Element("Transitions")?.Elements()?.ToList();
            Transitions?.ForEach(x => stateMachine._AddTransition(x, true));

            var Datas = stateMachineXElement.Element("DATA")?.Elements()?.ToList();
            Datas?.ForEach(x => stateMachine._AddData(x, true));
            stateMachine?._logger.LogDebug("StateMachine from XDocument");
            return stateMachine;
        }

        internal static StateMachine _FromXDocument(StateMachine stateMachine, string xDocumentPath, bool withLog)
        {
            xDocumentPath = Check.Name(xDocumentPath, stateMachine?._logger);
            var xDocument = XDocument.Load(xDocumentPath);
            return _FromXDocument(stateMachine, xDocument, withLog);
        }

        public static StateMachine FromXDocument(XDocument xDocument, ILogger logger = null)
        {
            var stateMachine = new StateMachine(logger);
            return _FromXDocument(stateMachine, xDocument, true);
        }

        public static StateMachine FromXDocument(string xmlFilePath, ILogger logger = null)
        {
            var stateMachine = new StateMachine(logger);
            return _FromXDocument(stateMachine, xmlFilePath, true);
        }

    }
}
