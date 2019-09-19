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
        

        public object Generate(Random random)
        {
            var generated = (double)random.NextDouble();
            return generated;
        }
    }
}
