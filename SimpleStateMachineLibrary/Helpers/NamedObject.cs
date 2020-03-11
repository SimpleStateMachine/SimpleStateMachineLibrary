namespace SimpleStateMachineLibrary.Helpers
{

    public abstract class NamedObject
    {
        public string Name { get; }

        public StateMachine StateMachine { get; }

        internal NamedObject(StateMachine stateMachine, string nameObject)
        {
            Name = Check.Name(nameObject, stateMachine?._logger);
            StateMachine = Check.Object(stateMachine, stateMachine?._logger);
        }
    }
}
