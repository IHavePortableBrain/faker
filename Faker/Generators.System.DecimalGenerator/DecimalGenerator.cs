using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class DecimalGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(decimal);
        public Random Random { get; private set; }

        public DecimalGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return NextDecimal(Random);
        }

        private int NextInt32(Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        private decimal NextDecimal(Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(NextInt32(rng),
                               NextInt32(rng),
                               NextInt32(rng),
                               sign,
                               scale);
        }
    }
}
