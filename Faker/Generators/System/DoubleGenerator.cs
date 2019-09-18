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
        protected Random Random = new Random();

        public object Generate()
        {
            return (double)Random.NextDouble();
        }
    }
}
