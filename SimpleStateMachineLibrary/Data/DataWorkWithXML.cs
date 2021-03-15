using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class Data
    {
        internal static XElement _ToXElement(Data data, bool withLog)
        {
            Check.NamedObject(data, data?.StateMachine?._logger);
            var element = new XElement("Data");
            element.Add(new XAttribute("Name", data.Name));
            element.Add(new XAttribute("Value", data.Value.ToString()));

            if(withLog)
                data.StateMachine._logger.LogDebug("Data \"{NameData}\" to XElement", data.Name);

            return element;
        }

        internal XElement _ToXElement(bool withLog)
        {
            return Data._ToXElement(this, withLog);
        }

        internal static IData _FromXElement(StateMachine stateMachine, XElement data, bool withLog)
        {
            stateMachine = Check.Object(stateMachine, stateMachine?._logger);
            data = Check.Object(data, stateMachine?._logger);

            var Name = data.Attribute("Name")?.Value;
            var Value = data.Attribute("Value")?.Value;

            var dataObj = stateMachine._AddData(Name, Value, null, result: out var result, exception:true, withLog: false);

            if((result)&&(withLog))
                stateMachine?._logger.LogDebug("Initialization data \"{NameData}\" from XElement", Name);

            return dataObj;
        }

    }
}
