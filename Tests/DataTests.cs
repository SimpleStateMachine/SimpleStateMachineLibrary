using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleStateMachineLibrary;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class DataTests
    {

        [TestMethod]
        [TestCategory("Data")]
        public void AllTestsWithLogger()
        {
            ForTest.stateMachine = new StateMachine(ForTest.GetConsoleLogger());
            AllTests();
        }

        [TestMethod]
        [TestCategory("Data")]
        public void AllTestsWithoutLogger()
        {
            ForTest.stateMachine = new StateMachine();
            AllTests();
        }

        public void AllTests()
        {
            AddDataTest();
            GetDataTest();
            DeleteDataTest();
            ChangeDataTest();
        }

        [TestMethod]
        [TestCategory("Data")]
        public void AddDataTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string dataName = "Data1";
            Assert.IsFalse(stateMachine.DataExists(dataName));

            Data data1 = stateMachine.AddData(dataName);
            Assert.IsNotNull(data1);

            Assert.IsTrue(stateMachine.DataExists(dataName));

            Assert.ThrowsException<ArgumentException>(() =>
            {
                data1 = stateMachine.AddData(dataName);
            });

            data1 = stateMachine.TryAddData(out bool result, dataName);
            Assert.IsNull(data1);
            Assert.IsFalse(result);

            stateMachine.DeleteData(dataName);
        }

        [TestMethod]
        [TestCategory("Data")]
        public void GetDataTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string dataName = "Data1";           
            Data data1;
            bool result;
            Assert.IsFalse(stateMachine.DataExists(dataName));

            //get data
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.GetData(dataName);
            Assert.IsNotNull(data1);
            Assert.IsTrue(stateMachine.DataExists(dataName));
            stateMachine.DeleteData(dataName);
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                data1 = stateMachine.GetData(dataName);
            });

            //try get data
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.TryGetData(dataName, out result);
            Assert.IsNotNull(data1);
            Assert.IsTrue(result);
            Assert.IsTrue(stateMachine.DataExists(dataName));
            stateMachine.DeleteData(dataName);
            data1 = stateMachine.TryGetData(dataName, out result);
            Assert.IsNull(data1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Data")]
        public void DeleteDataTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            string dataName = "Data1";
            Data data1;
            bool result;

            //delete data by name
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.DeleteData(dataName);
            Assert.IsNotNull(data1);
            Assert.IsFalse(stateMachine.DataExists(dataName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                data1 = stateMachine.DeleteData(dataName);
            });

            //delete data by object
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.DeleteData(data1);
            Assert.IsNotNull(data1);
            Assert.IsFalse(stateMachine.DataExists(dataName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                data1 = stateMachine.DeleteData(data1);
            });

            //try delete data by name
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.TryDeleteData(dataName, out result);
            Assert.IsNotNull(data1);
            Assert.IsTrue(result);
            data1 = stateMachine.TryDeleteData(dataName, out result);
            Assert.IsNull(data1);
            Assert.IsFalse(result);

            //try delete data by object
            data1 = stateMachine.AddData(dataName);
            data1 = stateMachine.TryDeleteData(data1, out result);
            Assert.IsNotNull(data1);
            Assert.IsTrue(result);
            data1 = stateMachine.TryDeleteData(data1, out result);
            Assert.IsNull(data1);
            Assert.IsFalse(result);

            //delete data from object
            data1 = stateMachine.AddData(dataName);
            data1 = data1.Delete();
            Assert.IsNotNull(data1);
            Assert.IsFalse(stateMachine.DataExists(dataName));
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                data1 = data1.Delete();
            });

            //try delete data from object
            data1 = stateMachine.AddData(dataName);
            data1 = data1.TryDelete(out result);
            Assert.IsNotNull(data1);
            Assert.IsTrue(result);
            Assert.IsFalse(stateMachine.DataExists(dataName));
            data1 = data1.TryDelete(out result);
            Assert.IsNull(data1);
            Assert.IsFalse(result);

        }

        [TestMethod]
        [TestCategory("Data")]
        public void ChangeDataTest()
        {
            StateMachine stateMachine = ForTest.stateMachine;
            int eventCount = 0;
            string dataName = "Data1";
            int newDataValue = 5;
            Data data1;

            eventCount = 0;
            data1 = stateMachine.AddData(dataName);
            data1.Value = 5;
            Assert.AreEqual(eventCount, 0);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName, 10, MethodOnChange);
            Assert.AreEqual(eventCount, 0);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName, 10).OnChange(MethodOnChange);
            Assert.AreEqual(eventCount, 0);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName);
            data1.OnChange(MethodOnChange);
            data1.Value = 5;
            Assert.AreEqual(eventCount, 1);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName, actionOnChange:MethodOnChange);
            data1.Value = 5;
            Assert.AreEqual(eventCount, 1);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName).OnChange(MethodOnChange);
            data1.Value = 5;
            Assert.AreEqual(eventCount, 1);
            stateMachine.DeleteData(dataName);

            eventCount = 0;
            data1 = stateMachine.AddData(dataName, actionOnChange:MethodOnChange).OnChange(MethodOnChange).OnChange(MethodOnChange);
            data1.OnChange(MethodOnChange);
            data1.OnChange(MethodOnChange);
            data1.Value = 5;
            Assert.AreEqual(eventCount, 5);
            stateMachine.DeleteData(dataName);

            void MethodOnChange(Data data, object newValue)
            {
                Assert.AreEqual(data.Name, dataName);
                Assert.AreEqual(newValue, newDataValue);
                eventCount++;
            }

        }

    }
}
