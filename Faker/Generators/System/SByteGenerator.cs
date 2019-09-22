using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class SByteGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(sbyte);
        public Random Random { get; private set; }

        public SByteGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return (sbyte)Random.Next();
        }
    }
}
