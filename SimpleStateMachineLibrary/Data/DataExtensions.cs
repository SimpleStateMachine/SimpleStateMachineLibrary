namespace SimpleStateMachineLibrary
{
    public static class DataExtensions
    {
        public static IData Delete(this IData data)
        {
            return null;
            // return this.StateMachine.DeleteData(this);
        }
        public static IData TryDelete(this IData data, out bool result)
        {
            result = false;
            return null;
            // return this.StateMachine.TryDeleteData(this, out result);
        }
    }
}