using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        private Data _GetData(string nameData, out bool result, bool exception)
        {
            var data_ = Check.GetElement(_data, nameData, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get data \"{NameData}\"", nameData);
            else
                _logger?.LogDebug("Try get data \"{NameData}\"", nameData);

            return data_;
        }

        private Data _GetData(Data data, out bool result, bool exception)
        {
            var data_ = Check.GetElement(_data, data, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Get data \"{NameData}\"", data.Name);
            else
                _logger?.LogDebug("Try get data \"{NameData}\"", data.Name);

            return data_;
        }


        public Data GetData(string nameData)
        {
            return _GetData(nameData, out bool result, true);
        }

        public Data TryGetData(string nameData, out bool result)
        {
            return _GetData(nameData, out result, false);
        }

        public Data TryGetData(Data data, out bool result)
        {
            return _GetData(data, out result, false);
        }



        internal Data _AddData(string nameData, object valueData, Action<Data, object> actionOnChange,  out bool result, bool exception)
        {
            //throw that element already contains  
            result = Check.NotContains(_data, nameData, this._logger, exception);
            
            if (!result)
                return null;

            return new Data(this, nameData, valueData, actionOnChange);
        }

        internal Data AddData(Data data, out bool result, bool exception)
        {
            //throw that element already contains 
            result = Check.NotContains(_data, data, this._logger, exception);
            
            if (!result)
                return null;

            _data.Add(data.Name, data);

            if (exception)
                _logger?.LogDebug("Add data \"{NameData}\"", data.Name);
            else
                _logger?.LogDebug("Try add data \"{NameData}\"", data.Name);

            return data;
        }

        internal Data AddData(XElement xElement)
        {
            return SimpleStateMachineLibrary.Data.FromXElement(this, Check.Object(xElement, this._logger));
        }


        public Data AddData(string nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out bool result,  true);
        }

        public Data TryAddData(out bool result, string nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out result, false);
        }

     

        private Data _DeleteData(Data data, out bool result, bool exception)
        {
            var data_ = Check.Remove(_data, data, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Delete data \"{NameData}\"", data.Name);
            else
                _logger?.LogDebug("Try delete data \"{NameData}\"", data.Name);

            return data_;
        }

        private Data _DeleteData(string dataName, out bool result, bool exception)
        {
            var data_ = Check.Remove(_data, dataName, this._logger, out result, exception);

            if (exception)
                _logger?.LogDebug("Delete data \"{NameData}\"", dataName);
            else
                _logger?.LogDebug("Try delete data \"{NameData}\"", dataName);


            return data_;
        }


        public Data DeleteData(string nameData)
        {
            return _DeleteData(nameData, out bool result,  true);
        }

        public Data DeleteData(Data data)
        {
            return _DeleteData(data, out bool result, true);
        }

        public Data TryDeleteData(string nameData, out bool result)
        {
            return _DeleteData(nameData, out result, false);
        }

        public Data TryDeleteData(Data data, out bool result)
        {
            return _DeleteData(data, out result, false);
        }
    }
}
