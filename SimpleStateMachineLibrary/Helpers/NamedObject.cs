using SimpleStateMachineLibrary.StateMachines;

namespace SimpleStateMachineLibrary.Helpers
{
    public abstract class NamedObject
    {
        public string Name { get; }
        public StateMachine StateMachine { get; }

        public NamedObject(StateMachine stateMachine, string nameObject)
        {
            StateMachine = Check.Object(stateMachine);

            Name = Check.Name(nameObject);          
        }

    }
}
