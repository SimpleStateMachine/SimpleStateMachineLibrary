using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;

namespace SimpleStateMachineLibrary
{
    public partial class Data : NamedObject, IData
    {
        private object _value;

        public object Value 
        {   get => _value;
            set 
            {
                _onChange?.Invoke(this, value);
                _value = value;
            }
        }

        private Action<Data, object> _onChange;

        internal Data(StateMachine stateMachine, string nameData, object valueData, Action<Data, object> actionOnChange, bool withLog) : base(stateMachine, nameData)
        {
            Value = valueData;

            //stateMachine?._logger.LogDebug("Create data \"{NameData}\" ", nameData);

            stateMachine._AddData(this, out _, true, withLog);

            if (actionOnChange != null)
            {
                OnChange(actionOnChange);
            }  
        }
        
        public Data OnChange(Action<Data, object> actionOnChange)
        {
            actionOnChange = Check.Object(actionOnChange, this.StateMachine?._logger);

            _onChange += actionOnChange;
            this.StateMachine._logger.LogDebug("Method \"{NameMethod}\" subscribe on change data \"{NameData}\"", actionOnChange.Method.Name, this.Name);
            return this;
        }
    }
}
