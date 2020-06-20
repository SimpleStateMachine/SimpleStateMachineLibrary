using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {

        internal Data _GetData(string nameData, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.GetElement(_data, nameData, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get data \"{NameData}\"", nameData);
                else
                    _logger.LogDebug("Try get data \"{NameData}\"", nameData);
            }

            return data_;
        }

        internal string _DataExists(string nameData, out bool result, bool exeption, bool withLog)
        {
            return Check.Contains(_data, nameData, this._logger, out result, exeption);
        }

        public bool DataExists(string nameData)
        {
            nameData = _DataExists(nameData, out bool result, false, true);
            return result;
        }

        public Data GetData(string nameData)
        {
            return _GetData(nameData, out bool result, true, true);
        }

        public Data TryGetData(string nameData, out bool result)
        {
            return _GetData(nameData, out result, false, true);
        }

        internal Data _AddData(string nameData, object valueData, Action<Data, object> actionOnChange,  out bool result, bool exception, bool withLog)
        {
            //throw that element already contains  
            result = Check.NotContains(_data, nameData, this._logger, exception);
            
            if (!result)
                return null;

            return new Data(this, nameData, valueData, actionOnChange, withLog);
        }

        internal Data _AddData(Data data, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(_data, data, this._logger, exception);
            
            if (!result)
                return null;

            _data.Add(data.Name, data);
            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add data \"{NameData}\"", data.Name);
                else
                    _logger.LogDebug("Try add data \"{NameData}\"", data.Name);
            }

            return data;
        }

        internal Data _AddData(XElement xElement, bool withLog)
        {
            return Data._FromXElement(this, Check.Object(xElement, this._logger), withLog);
        }


        public Data AddData(string nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out bool result, true, true);
        }

        public Data TryAddData(out bool result, string nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out result, false, true);
        }

     

        private Data _DeleteData(Data data, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove(_data, data, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", data.Name);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", data.Name);
            }

            return data_;
        }

        private Data _DeleteData(string dataName, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove(_data, dataName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", dataName);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", dataName);
            }


            return data_;
        }


        public Data DeleteData(string nameData)
        {
            return _DeleteData(nameData, out bool result,  true, true);
        }

        public Data DeleteData(Data data)
        {
            return _DeleteData(data, out bool result, true, true);
        }

        public Data TryDeleteData(string nameData, out bool result)
        {
            return _DeleteData(nameData, out result, false, true);
        }

        public Data TryDeleteData(Data data, out bool result)
        {
            return _DeleteData(data, out result, false, true);
        }
    }
}
