using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Faker.UseExample
{
    class UseExample
    {
        static void Main(string[] args)
        {
            CheckT<DateTime>();
            CheckT<int>();
            CheckT<string>();
            Faker _faker = new Faker();
            int i = _faker.Create<int>();
            sbyte s = _faker.Create<sbyte>();
            string str = _faker.Create<string>();
            Console.ReadKey();
        }

        private static void CheckT<T>()
        {
            Console.WriteLine(typeof(T));
        }
    }
}
