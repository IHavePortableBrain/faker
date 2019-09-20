using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Faker.UseExample
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
        private string str;
        

        public Foo(int i, string str)
        {
            this.l = i;
            this.str = str;
        }
    }

    public class Bar
    {
        public long l;
        public DateTime DateTime;
        public Foo foo;
        public System.Xml.Serialization.CodeExporter CodeExporter;
        public System.Xml.Serialization.XmlSerializer XmlSerializer;
    }

    class UseExample
    {
        struct MyStruct1
        {

        }

        static void Main(string[] args)
        {
            //CheckT<DateTime>();
            //CheckT<int>();
            //CheckT<string>();
            Type t1 = typeof(MyEnum);
            Type t2 = typeof(MyStruct);
            Type t3 = typeof(MyStruct1);
            Faker _faker = new Faker();
            Bar bar = _faker.Create<Bar>();
            System.Xml.Serialization.XmlSerializer xmlSerializer = _faker.Create<System.Xml.Serialization.XmlSerializer>();
            Console.ReadKey();
        }

        private static void CheckT<T>()
        {
            Console.WriteLine(typeof(T));
        }
    }
}
