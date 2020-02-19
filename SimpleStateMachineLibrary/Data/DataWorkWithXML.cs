using SimpleStateMachineLibrary.Helpers;
using SimpleStateMachineLibrary.StateMachines;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary.Datas
{
    public partial class Data : NamedObject
    {

        public static XElement ToXElement(Data data)
        {
            Check.NamedObject(data);
            XElement element = new XElement("Data");
            element.Add(new XAttribute("Name", data.Name));
            element.Add(new XAttribute("Value", data.Value.ToString()));
            return element;
        }

        public XElement ToXElement()
        {
            XElement element = new XElement("Transition");
            element.Add(new XAttribute("Name", this.Name));
            element.Add(new XAttribute("Value", this.Value.ToString()));
            return element;
        }

        public static Data FromXElement(StateMachine stateMachine, XElement data)
        {
            stateMachine = Check.Object(stateMachine);
            data = Check.Object(data);

            string Name = data.Attribute("Name")?.Value;
            string Value = data.Attribute("Value")?.Value;
            return stateMachine.AddData(Name, Value);
        }
    }
}
