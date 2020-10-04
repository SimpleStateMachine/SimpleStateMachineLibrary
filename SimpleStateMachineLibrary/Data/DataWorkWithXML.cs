using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;


namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {
        public partial class Data
        {
            internal static XElement _ToXElement(Data data, bool withLog)
            {
                Check.NamedObject<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(data, data?.StateMachine?._logger);
                XElement element = new XElement("Data");
                element.Add(new XAttribute("Name", data.Name));
                element.Add(new XAttribute("Value", data.Value.ToString()));

                if (withLog)
                    data.StateMachine._logger.LogDebug("Data \"{NameData}\" to XElement", data.Name);

                return element;
            }

            internal XElement _ToXElement(bool withLog)
            {
                return Data._ToXElement(this, withLog);
            }

            internal static Data _FromXElement(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine, XElement data, bool withLog)
            {
                stateMachine = Check.Object(stateMachine, stateMachine?._logger);
                data = Check.Object(data, stateMachine?._logger);

                //GMIKE
                //TKeyData Name = (TKeyData)data.Attribute("Name")?.Value;
                TKeyData Name = default;
                string Value = data.Attribute("Value")?.Value;

                Data dataObj = stateMachine._AddData(Name, Value, null, result: out bool result, exception: true, withLog: false);

                if ((result) && (withLog))
                    stateMachine?._logger.LogDebug("Initialization data \"{NameData}\" from XElement", Name);

                return dataObj;
            }

        }
    }
}
