using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace SpringAOPDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化Spring.Net容器
            IApplicationContext ctx = ContextRegistry.GetContext();

            //使用spring.net  aop代码
            IMathService mathService = ctx.GetObject("MathService") as IMathService;
            mathService.add(4, 5);


            Console.ReadKey();


        }
    }
}
