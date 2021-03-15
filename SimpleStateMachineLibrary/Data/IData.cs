using System;

namespace SimpleStateMachineLibrary
{
    public interface IData
    {
        string Name { get; }
        object Value { get; } 
    }
}