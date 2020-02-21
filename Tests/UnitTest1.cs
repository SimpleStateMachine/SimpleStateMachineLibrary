using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stateMachine = new StateMachine();
            var t = stateMachine.TryGetTransitionsFromState("trtr");
            //stateMachine.AddData("ttt");
            //stateMachine.TryAddData("ttt");


        }
    }
}
