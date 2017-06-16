using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WWFWinFormDemo
{

    public sealed class InputMoneyActivity : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public OutArgument<int> OutMoney { get; set; }
        public InArgument<string> InBookMarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            //int moeny = int.Parse(Console.ReadLine());


            //1.让工作流停下来

            //2.创建书签
            string bookMarkName = context.GetValue(InBookMarkName);

            context.CreateBookmark(bookMarkName, new BookmarkCallback(CallBackFunc));

            //context.SetValue(outMoney, moeny);

        }

        private void CallBackFunc(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutMoney,(int)value);
        }
    }
}
