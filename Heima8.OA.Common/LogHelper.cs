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
        public static List<ILogWriter> LogiWriters = new List<ILogWriter>(); 
         
        static LogHelper()
        {
            //LogiWriters.Add(new SqlServerWriter());
            //LogiWriters.Add(new TextFileWriter());


            LogiWriters.Add(new Log4NetWriter());

            ThreadPool.QueueUserWorkItem(o =>
            {

                while (true)
                {
                    
                    lock (ExceptionStringQueue)
                    {
                        if (ExceptionStringQueue.Count > 0)
                        {
                            string strinfo = ExceptionStringQueue.Dequeue();
                            foreach (var logWriter in LogiWriters)
                            {
                                logWriter.WriteLogInfo(strinfo);
                            }                            
                        }
                        else
                        {
                            Thread.Sleep(30);
                        }

                    }
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
