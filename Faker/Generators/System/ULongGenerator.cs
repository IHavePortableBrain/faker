using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ULongGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(ulong);
        public Random Random { get; private set; }

        public ULongGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }
    }
}
