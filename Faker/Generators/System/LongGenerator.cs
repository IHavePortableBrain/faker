using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class LongGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(long);
        public Random Random { get; private set; }

        public LongGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }
    }
}
