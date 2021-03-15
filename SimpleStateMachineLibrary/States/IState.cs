namespace SimpleStateMachineLibrary
{
    public interface IState
    {
        StateMachine StateMachine { get;}
        string Name { get; }
    }
}