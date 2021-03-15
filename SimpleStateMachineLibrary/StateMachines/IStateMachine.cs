using System.Collections.Generic;

namespace SimpleStateMachineLibrary
{
    public interface IStateMachine
    {
        // Dictionary<string, IState> States { get; }
        // Dictionary<string, ITransition> Transitions { get; }
        // Dictionary<string, IData> Data { get; }
        IState CurrentState { get; }

        IState PreviousState { get; }
        
        ITransition CurrentTransition { get;}
        
        ITransition NextTransition { get; }
        
        IState StartState { get; }
    }
}