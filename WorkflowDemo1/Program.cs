using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace WorkflowDemo1
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity LeaveActivity = new LeaveActivity();
            WorkflowInvoker.Invoke(LeaveActivity);
            Console.Read();
        }
    }
}
