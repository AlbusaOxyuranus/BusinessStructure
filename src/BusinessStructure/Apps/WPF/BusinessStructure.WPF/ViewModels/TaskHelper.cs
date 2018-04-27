using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessStructure.Vms
{
    public class TaskHelper
    {
        public static Task RunAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            });
            return tcs.Task;
        }

    }
}
