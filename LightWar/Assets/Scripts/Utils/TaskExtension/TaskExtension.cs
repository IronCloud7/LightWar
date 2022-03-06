using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships.Utilities.TaskExtensions
{
    public static class TaskExtension
    {
        public static async void WrapErrors(this Task task)
        {
            await task;
        }
    }
}
