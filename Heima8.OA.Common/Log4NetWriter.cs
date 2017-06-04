using log4net;

namespace Heima8.OA.Common
{
    public class Log4NetWriter : ILogWriter
    {

        public void WriteLogInfo(string txt)
        {
            ILog logWriter = log4net.LogManager.GetLogger("Demo"); //"Demo相当于当前logwriter的key标识，不是从配置里面读到的，就是自己设置的"
            
            logWriter.Error(txt);
        }
    }
}