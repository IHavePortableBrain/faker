using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ShortGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(short);
        public Random Random { get; private set; }

        public ShortGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return (short)Random.Next();
        }
    }
}
