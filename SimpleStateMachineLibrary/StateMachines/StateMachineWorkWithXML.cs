﻿using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Linq;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {

        internal static XDocument _ToXDocument(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine, string nameFile, bool withLog)
        {          
            Check.Object(stateMachine, stateMachine?._logger);
            Check.Name(nameFile, stateMachine?._logger);
            XDocument xDocument = new XDocument();  
            XElement stateMachineXElement = new XElement("StateMachine");
            xDocument.Add(stateMachineXElement);
            stateMachine?._logger.LogDebug("StateMachine to XDocument");
            XElement states = new XElement("States");
            stateMachineXElement.Add(states);
            foreach(var state in stateMachine._states)
            {
                states.Add(state.Value.ToXElement(withLog));
            }
            
            if (!object.Equals(stateMachine == null ? default(TKeyState) : stateMachine._startState, default(TKeyState)))
            {
                XElement startState = new XElement("StartState");
                stateMachineXElement.Add(startState);
                startState.Add(new XAttribute("Name", stateMachine._startState));
            }

            XElement transitions = new XElement("Transitions");
            stateMachineXElement.Add(transitions);

            foreach (var transition in stateMachine._transitions)
            {
                transitions.Add(transition.Value._ToXElement(withLog));
            }

            XElement datas = new XElement("DATA");
            stateMachineXElement.Add(datas);

            foreach (var data in stateMachine._data)
            {
                datas.Add(data.Value._ToXElement(withLog));
            }

            xDocument.Save(nameFile);
       
            return xDocument;
        }

        public XDocument ToXDocument(string nameFile)
        {
            return StateMachine<TKeyState, TKeyTransition, TKeyData>._ToXDocument(this, nameFile, true);
        }

        internal static StateMachine<TKeyState, TKeyTransition, TKeyData> _FromXDocument(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine, XDocument xDocument, bool withLog)
        {
            XElement stateMachineXElement = Check.Object(xDocument, stateMachine?._logger).Element("StateMachine");
            stateMachineXElement = Check.Object(stateMachineXElement, stateMachine?._logger);
            var States = stateMachineXElement.Element("States")?.Elements()?.ToList();
            States?.ForEach(x => stateMachine._AddState(x, true));
            var startState = stateMachineXElement.Element("StartState");
            //GMIKE
            //string nameStartState = startState?.Attribute("Name").Value;
            TKeyState nameStartState = default;
            if (!object.Equals(nameStartState, default(TKeyState)))
                stateMachine.SetStartState(nameStartState);

            var Transitions = stateMachineXElement.Element("Transitions")?.Elements()?.ToList();
            Transitions?.ForEach(x => stateMachine._AddTransition(x, true));

            var Datas = stateMachineXElement.Element("DATA")?.Elements()?.ToList();
            Datas?.ForEach(x => stateMachine._AddData(x, true));
            stateMachine?._logger.LogDebug("StateMachine from XDocument");
            return stateMachine;
        }

        internal static StateMachine<TKeyState, TKeyTransition, TKeyData> _FromXDocument(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine, string xDocumentPath, bool withLog)
        {
            xDocumentPath = Check.Name(xDocumentPath, stateMachine?._logger);
            XDocument xDocument = XDocument.Load(xDocumentPath);
            return _FromXDocument(stateMachine, xDocument, withLog);
        }

        public static StateMachine<TKeyState, TKeyTransition, TKeyData> FromXDocument(XDocument xDocument, ILogger logger = null)
        {
            StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine = new StateMachine<TKeyState, TKeyTransition, TKeyData>(logger);
            return _FromXDocument(stateMachine, xDocument, true);
        }

        public static StateMachine<TKeyState, TKeyTransition, TKeyData> FromXDocument(string xmlFilePath, ILogger logger = null)
        {
            StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine = new StateMachine<TKeyState, TKeyTransition, TKeyData>(logger);
            return _FromXDocument(stateMachine, xmlFilePath, true);
        }

    }
}
