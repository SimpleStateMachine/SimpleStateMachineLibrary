namespace SimpleStateMachineLibrary
{
    public interface ITransition
    {
        string Name { get; }
        IState StateFrom { get;}

        IState StateTo { get;}
    }
}