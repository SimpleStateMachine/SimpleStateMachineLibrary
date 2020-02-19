using SimpleStateMachineLibrary.Helpers;
using SimpleStateMachineLibrary.StateMachines;


namespace SimpleStateMachineLibrary.Datas
{
    public partial class Data : NamedObject
    {
        public object Value { get; set; }

        public Data(StateMachine stateMachine, string nameData, object valueData = null) : base(stateMachine, nameData)
        {
            Value = valueData;
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
