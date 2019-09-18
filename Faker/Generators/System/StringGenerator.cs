using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class StringGenerator : IGenerator
    {
        const int MinLength = 3;
        const int MaxLength = 20;
        public Type TypeOfGenerated => typeof(string);
        protected Random Random = new Random();


        public object Generate()
        {
            int length = Random.Next(MinLength, MaxLength);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
