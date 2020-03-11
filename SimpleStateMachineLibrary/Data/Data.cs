using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary.Helpers;
using System;

namespace SimpleStateMachineLibrary
{
    public partial class Data : NamedObject
    {
        private object _value;

        public object Value 
        {   get { return _value; }
            set 
            {
                _onChange?.Invoke(this, value, value);
                _value = value;
            }
        }

        private Action<Data, object, object> _onChange;

        internal Data(StateMachine stateMachine, string nameData, object valueData, Action<Data, object, object> actionOnChange) : base(stateMachine, nameData)
        {
            Value = valueData;

            stateMachine?._logger?.LogDebug("Create data \"{NameData}\" ", nameData);

            if (actionOnChange != null)
            {
                OnChange(actionOnChange);
            }

            stateMachine.AddData(this, out bool result, true);
        }

        public Data Delete()
        {
            return this.StateMachine.DeleteData(this);
        }

        public Data TryDelete(out bool result)
        {
            return this.StateMachine.TryDeleteData(this, out result);
        }

        public Data OnChange(Action<Data, object, object> actionOnChange)
        {
            actionOnChange = Check.Object(actionOnChange, this.StateMachine?._logger);

            _onChange += actionOnChange;
            this.StateMachine._logger?.LogDebugAndInformation("Method \"{NameMethod}\" subscribe on change data \"{NameData}\"", actionOnChange.Method.Name, this.Name);
            return this;
        }
    }
}
