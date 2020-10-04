using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachineLibrary
{
    public interface IStateMachine
    {
        bool DataExists<TKeyData>(TKeyData nameData);
    }
}
