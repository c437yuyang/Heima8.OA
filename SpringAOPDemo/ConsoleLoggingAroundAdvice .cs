using System;
using AopAlliance.Intercept;

namespace SpringAOPDemo
{
    public class ConsoleLoggingAroundAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            Console.Out.WriteLine("Advice executing; calling the advised method...");
            object returnValue = invocation.Proceed();//执行咱们的方法。
            Console.Out.WriteLine("Advice executed; advised method returned " + returnValue);
            return returnValue;
        }
    }
}
