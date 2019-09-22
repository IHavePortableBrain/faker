using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class DoubleGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(double);
        public Random Random { get; private set; }

        public DoubleGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            var generated = (double)Random.NextDouble();
            return generated;
        }
    }
}
