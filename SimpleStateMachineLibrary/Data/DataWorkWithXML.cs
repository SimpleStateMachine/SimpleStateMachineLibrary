using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Data
    {
        public static XElement ToXElement(Data data)
        {
            Check.NamedObject(data);
            XElement element = new XElement("Data");
            element.Add(new XAttribute("Name", data.Name));
            element.Add(new XAttribute("Value", data.Value.ToString()));
            data.StateMachine._logger?.LogDebug("Data \"{NameData}\" to XElement", data.Name);
            return element;
        }

        public XElement ToXElement()
        {
            return Data.ToXElement(this);
        }

        public static Data FromXElement(StateMachine stateMachine, XElement data)
        {
            stateMachine = Check.Object(stateMachine);
            data = Check.Object(data);

            string Name = data.Attribute("Name")?.Value;
            string Value = data.Attribute("Value")?.Value;

            stateMachine._logger?.LogDebug("Initialization data \"{NameData}\" from XElement", Name);
            return stateMachine.AddData(Name, Value);
        }

    }
}
