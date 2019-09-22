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
        public Random Random { get; private set; }

        public StringGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            int length = Random.Next(MinLength, MaxLength);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
