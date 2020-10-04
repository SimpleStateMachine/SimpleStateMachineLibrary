using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine<TKeyState, TKeyTransition, TKeyData>
    {

        internal Data _GetData(TKeyData nameData, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.GetElement<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, nameData, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Get data \"{NameData}\"", nameData);
                else
                    _logger.LogDebug("Try get data \"{NameData}\"", nameData);
            }

            return data_;
        }

        internal TKeyData _DataExists(TKeyData nameData, out bool result, bool exeption, bool withLog)
        {
            return Check.Contains<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, nameData, this._logger, out result, exeption);
        }

        public bool DataExists(TKeyData nameData)
        {
            nameData = _DataExists(nameData, out bool result, false, true);
            return result;
        }

        public Data GetData(TKeyData nameData)
        {
            return _GetData(nameData, out bool result, true, true);
        }

        public Data TryGetData(TKeyData nameData, out bool result)
        {
            return _GetData(nameData, out result, false, true);
        }

        internal Data _AddData(TKeyData nameData, object valueData, Action<Data, object> actionOnChange,  out bool result, bool exception, bool withLog)
        {
            //throw that element already contains  
            result = Check.NotContains<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, nameData, this._logger, exception);
            
            if (!result)
                return null;

            return new Data(this, nameData, valueData, actionOnChange, withLog);
        }

        internal Data _AddData(Data data, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, data, this._logger, exception);
            
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


        public Data AddData(TKeyData nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out bool result, true, true);
        }

        public Data TryAddData(out bool result, TKeyData nameData, object valueData = default(object), Action<Data, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out result, false, true);
        }

     

        private Data _DeleteData(Data data, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, data, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", data.Name);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", data.Name);
            }

            return data_;
        }

        private Data _DeleteData(TKeyData dataName, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove<TKeyData, TKeyState, TKeyTransition, TKeyData, Data>(_data, dataName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", dataName);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", dataName);
            }


            return data_;
        }


        public Data DeleteData(TKeyData nameData)
        {
            return _DeleteData(nameData, out bool result,  true, true);
        }

        public Data DeleteData(Data data)
        {
            return _DeleteData(data, out bool result, true, true);
        }

        public Data TryDeleteData(TKeyData nameData, out bool result)
        {
            return _DeleteData(nameData, out result, false, true);
        }

        public Data TryDeleteData(Data data, out bool result)
        {
            return _DeleteData(data, out result, false, true);
        }
    }
}
