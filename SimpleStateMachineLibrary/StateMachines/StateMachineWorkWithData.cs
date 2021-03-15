using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;
using System.Xml.Linq;

namespace SimpleStateMachineLibrary
{
    public partial class StateMachine
    {
        public bool DataExists(string nameData)
        {
            nameData = _DataExists(nameData, out var result, false, true);
            return result;
        }
        public IData GetData(string nameData)
        {
            return _GetData(nameData, out var result, true, true);
        }
        public IData TryGetData(string nameData, out bool result)
        {
            return _GetData(nameData, out result, false, true);
        }
        public IData AddData(string nameData, object valueData = default(object), Action<IData, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out var result, true, true);
        }
        public IData TryAddData(out bool result, string nameData, object valueData = default(object), Action<IData, object> actionOnChange = null)
        {
            return _AddData(nameData, valueData, actionOnChange, out result, false, true);
        }
        public IData DeleteData(string nameData)
        {
            return _DeleteData(nameData, out var result,  true, true);
        }
        public IData DeleteData(IData data)
        {
            return _DeleteData(data, out var result, true, true);
        }
        public IData TryDeleteData(string nameData, out bool result)
        {
            return _DeleteData(nameData, out result, false, true);
        }
        
        
        
        
        //TODO AddData without Name
        internal IData _GetData(string nameData, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.GetElement(IData, nameData, this._logger, out result, exception);

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
            return Check.Contains(IData, nameData, this._logger, out result, exeption);
        }
        
        
        internal IData _AddData(string nameData, object valueData, Action<IData, object> actionOnChange,  out bool result, bool exception, bool withLog)
        {
            //throw that element already contains  
            result = Check.NotContains(IData, nameData, this._logger, exception);
            
            if (!result)
                return null;

            return new IData(this, nameData, valueData, actionOnChange, withLog);
        }
        internal IData _AddData(IData data, out bool result, bool exception, bool withLog)
        {
            //throw that element already contains 
            result = Check.NotContains(IData, data, this._logger, exception);
            
            if (!result)
                return null;

            IData.Add(data.Name, data);
            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Add data \"{NameData}\"", data.Name);
                else
                    _logger.LogDebug("Try add data \"{NameData}\"", data.Name);
            }

            return data;
        }
        internal IData _AddData(XElement xElement, bool withLog)
        {
            return SimpleStateMachineLibrary.IData._FromXElement(this, Check.Object(xElement, this._logger), withLog);
        }
        
        
        private IData _DeleteData(IData data, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove(IData, data, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", data.Name);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", data.Name);
            }

            return data_;
        }

        private IData _DeleteData(string dataName, out bool result, bool exception, bool withLog)
        {
            var data_ = Check.Remove(IData, dataName, this._logger, out result, exception);

            if (withLog)
            {
                if (exception)
                    _logger.LogDebug("Delete data \"{NameData}\"", dataName);
                else
                    _logger.LogDebug("Try delete data \"{NameData}\"", dataName);
            }


            return data_;
        }
        
    }
}
