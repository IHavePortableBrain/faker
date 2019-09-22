using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ByteGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(byte);
        public Random Random { get; private set; }

        public ByteGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return (byte)Random.Next();
        }
    }
}
