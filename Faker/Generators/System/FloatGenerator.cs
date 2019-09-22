using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class FloatGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(float);
        public Random Random { get; private set; }

        public FloatGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            var generated = (float)Random.NextDouble();
            return generated;
        }
    }
}
