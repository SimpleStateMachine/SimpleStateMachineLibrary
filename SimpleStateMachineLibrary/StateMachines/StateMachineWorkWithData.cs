using SimpleStateMachineLibrary.Datas;
using SimpleStateMachineLibrary.Helpers;


namespace SimpleStateMachineLibrary.StateMachines
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

        public Data Data(Data data)
        {
            return _Data(data, true);
        }

        public Data TryGetData(string nameData)
        {
            return _Data(nameData, false);
        }

        public Data TryGetData(Data data)
        {
            return _Data(data, false);
        }

        private Data _AddData(Data data, bool exeption)
        {
            return Check.AddElement(_data, data, exeption);
        }

        private Data _AddData(string nameData, object valueData, bool exeption)
        {
            if (!Check.NotContains(_data, nameData, exeption))
                return null;

            Data newData = new Data(this, nameData, valueData);
            _data.Add(nameData, newData);
            return newData;
        }

        public Data AddData(string nameData, object valueData = default(object))
        {
            return _AddData(nameData, valueData, true);
        }

        public Data AddData(Data data)
        {
            return _AddData(data, true);
        }
        //public Data AddData(XElement xElement)
        //{
        //    return StateMachines.Data.FromXElement(this, xElement);
        //}
        public Data TryAddData(string nameData, object valueData = default(object))
        {
            return _AddData(nameData, valueData, false);
        }

        public Data TryAddData(Data data)
        {
            return _AddData(data, false);
        }


        private Data _DeleteData(Data state, bool exeption)
        {
            return Check.Remove(_data, state, exeption);
        }

        private Data _DeleteData(string stateName, bool exeption)
        {
            return Check.Remove(_data, stateName, exeption);
        }

        public Data DeleteData(string nameData)
        {
            return _DeleteData(Data(nameData), true);
        }

        public Data DeleteData(Data data)
        {
            return _DeleteData(Data(data),true);
        }

        public Data TryDeleteData(string nameData)
        {
            return _DeleteData(Data(nameData), false);
        }

        public Data TryDeleteData(Data data)
        {
            return _DeleteData(Data(data), false);
        }
    }
}
