using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public struct MyStruct
    {
        public int a;
        public decimal b;
    }

    public enum MyEnum
    {
        e0, e1, e2, e3, e4, e5
    }

    public class Foo
    {
        public MyEnum myEnum;
        public MyStruct myStruct;
        private long l;
        sbyte sb;
        public string str;
    }
}
