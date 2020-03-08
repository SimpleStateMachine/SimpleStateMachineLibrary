using SimpleStateMachineLibrary.Helpers;
using System;

namespace SimpleStateMachineLibrary
{
    public partial class Data : NamedObject<Data>
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

        public Data OnChange(Action<Data, object, object> actionOnChange)
        {
            _onChange += actionOnChange;

            return this;
        }
        protected internal Data(StateMachine stateMachine, string nameData, object valueData = null) : base(stateMachine, nameData)
        {
            Value = valueData;

            stateMachine.AddData(this, true);
        }

        public Data Delete()
        {
            return this.StateMachine.DeleteData(this);
        }

        public Data TryDelete()
        {
            return this.StateMachine.TryDeleteData(this);
        }
    }
}
