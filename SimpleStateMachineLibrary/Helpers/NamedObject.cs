namespace SimpleStateMachineLibrary.Helpers
{

    public abstract class NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
    {
        public TName Name { get; }

        public StateMachine<TKeyState, TKeyTransition, TKeyData> StateMachine { get; }

        internal NamedObject(StateMachine<TKeyState, TKeyTransition, TKeyData> stateMachine, TName nameObject)
        {
            Name = Check.Name(nameObject, stateMachine?._logger);
            StateMachine = Check.Object(stateMachine, stateMachine?._logger);
        }
    }
}
