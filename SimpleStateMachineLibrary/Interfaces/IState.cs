using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachineLibrary
{
    public interface IState<TName>
    {
         TName Name { get; }
    }
}
