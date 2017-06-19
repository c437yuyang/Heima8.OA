using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringAOPDemo
{
    public class MathService : IMathService
    {
        public int add(int a, int b)
        {
            return a + b;
        }

        public int doubleAdd(int a)
        {
            return a + a;
        }
    }
}
