namespace SimpleStateMachineLibrary.Helpers
{

    public abstract class NamedObject<TObject>
    {
        public string Name { get; }

        public StateMachine StateMachine { get; }

        protected internal NamedObject(StateMachine stateMachine, string nameObject)
        {
            Name = Check.Name(nameObject);
            StateMachine = Check.Object(stateMachine);
        }
    }
}
