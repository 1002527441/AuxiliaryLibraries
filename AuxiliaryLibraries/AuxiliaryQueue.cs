using System;
using System.Collections.Generic;

namespace AuxiliaryLibraries
{
    public static class AuxiliaryQueue
    {
        public static void Push<T>(this Queue<object> queue, List<T> list)
        {
            try
            {
                foreach (var item in list)
                {
                    queue.Enqueue(item);
                }
            }
            catch (Exception e)
            {
                //TODO: Log e
                var stackTrace = e.StackTrace;
                var error = e.ToString();
            }
        }

        public static object Pop(this Queue<object> queue)
        {
            var dequed = new object();
            try
            {
                dequed = queue.Dequeue();
            }
            catch (Exception e)
            {
                //TODO: Log e
                var stackTrace = e.StackTrace;
                var error = e.ToString();
            }

            return dequed;
        }
    }
}
