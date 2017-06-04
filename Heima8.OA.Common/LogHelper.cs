using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heima8.OA.Common
{
    public class LogHelper
    {

        public static Queue<string> ExceptionStringQueue = new Queue<string>();

         
        static LogHelper()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    string str = ExceptionStringQueue.Dequeue();

                }
            });
        }

        public static void writeLog(string execeptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(execeptionText);
            }
        }


    }
}
