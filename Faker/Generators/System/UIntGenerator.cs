using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class UIntGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(uint);
        public Random Random { get; private set; }

        public UIntGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
    }
}
