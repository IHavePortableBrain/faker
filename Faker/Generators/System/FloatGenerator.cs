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
        

        public object Generate(Random random)
        {
            var generated = (float)random.NextDouble();
            return generated;
        }
    }
}
