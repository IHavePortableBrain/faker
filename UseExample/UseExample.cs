using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Faker.UseExample
{
    public class Foo
    {
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
        public string str;
        public bool booll;
        public byte bytee;
        public decimal deci;
        public double doubl;
        public float floatt;
        public ulong ulongg;
        public ushort ushortt;
        public uint uyntt;

    }

    class UseExample
    {
        static void Main(string[] args)
        {
            //CheckT<DateTime>();
            //CheckT<int>();
            //CheckT<string>();
            Faker _faker = new Faker();
            Foo foo = _faker.Create<Foo>();
            Bar bar = _faker.Create<Bar>();
            Console.ReadKey();
        }

        private static void CheckT<T>()
        {
            Console.WriteLine(typeof(T));
        }
    }
}
