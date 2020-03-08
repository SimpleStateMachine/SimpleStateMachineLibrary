


using SimpleStateMachineLibrary.Helpers;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        private Data _Data(string nameData, bool exeption)
        {
            return Check.GetElement(_data, nameData, exeption);
        }

        private Data _Data(Data data, bool exeption)
        {
            return Check.GetElement(_data, data, exeption);
        }

        public Data Data(string nameData)
        {
            return _Data(nameData, true);
        }

        public Data TryGetData(string nameData)
        {
            return _Data(nameData, false);
        }

        public Data TryGetData(Data data)
        {
            return _Data(data, false);
        }


        private Data _AddData(string nameData, object valueData, bool exeption)
        {
            if (!Check.NotContains(_data, nameData, exeption))
                return null;

            return new Data(this, nameData, valueData);
        }

        internal Data AddData(Data data, bool exeption)
        {
            if (!Check.NotContains(_data, data, exeption))
                return null;

            _data.Add(data.Name, data);
            return data;
        }

        public Data AddData(string nameData, object valueData = default(object))
        {
            return _AddData(nameData, valueData, true);
        }

        public Data TryAddData(string nameData, object valueData = default(object))
        {
            return _AddData(nameData, valueData, false);
        }

        public Data AddData(XElement xElement)
        {
            return SimpleStateMachineLibrary.Data.FromXElement(this, Check.Object(xElement));
        }

        private Data _DeleteData(Data data, bool exeption)
        {
            return Check.Remove(_data, data, exeption);
        }

        private Data _DeleteData(string dataName, bool exeption)
        {
            return Check.Remove(_data, dataName, exeption);
        }

        public Data DeleteData(string nameData)
        {
            return _DeleteData(nameData, true);
        }

        public Data DeleteData(Data data)
        {
            return _DeleteData(data, true);
        }

        public Data TryDeleteData(string nameData)
        {
            return _DeleteData(nameData, false);
        }

        public Data TryDeleteData(Data data)
        {
            return _DeleteData(data, false);
        }
    }
}
