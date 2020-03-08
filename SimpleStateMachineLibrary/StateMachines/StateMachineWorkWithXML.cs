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
            var States = stateMachineXElement.Element("States")?.Elements()?.ToList();
            States.ForEach(x => stateMachine.AddState(x));
            var startState = stateMachineXElement.Element("StartState");
            string nameStartState = startState?.Attribute("Name").Value;

            if (!string.IsNullOrEmpty(nameStartState))
                stateMachine.SetStartState(nameStartState);

            var Transitions = stateMachineXElement.Element("Transitions")?.Elements()?.ToList();
            Transitions.ForEach(x => stateMachine.AddTransition(x));
           
            return stateMachine;
        }


        public static StateMachine FromXDocument(StateMachine stateMachine, string xDocumentPath)
        {
            xDocumentPath = Check.Name(xDocumentPath);
            XDocument xDocument = XDocument.Load(xDocumentPath);
            return FromXDocument(stateMachine, xDocument);
        }

        public static StateMachine FromXDocument(XDocument xDocument)
        {
            StateMachine stateMachine = new StateMachine();
            return FromXDocument(stateMachine, xDocument);
        }

        public static StateMachine FromXDocument(string xDocumentPath)
        {
            StateMachine stateMachine = new StateMachine();
            return FromXDocument(stateMachine, xDocumentPath);
        }

    }
}
