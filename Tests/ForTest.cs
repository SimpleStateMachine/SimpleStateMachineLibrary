using Microsoft.Extensions.Logging;
using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public static class ForTest
    {

        static public StateMachine stateMachine = new StateMachine();
        public static ILogger<StateMachine> GetConsoleLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug); });
            return loggerFactory.CreateLogger<StateMachine>();
        }

    }
}
