using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class IntGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(int);
        public Random Random { get; private set; }

        public IntGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return Random.Next();
        }
    }
}
