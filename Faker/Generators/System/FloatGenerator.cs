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
        protected Random Random = new Random();

        public object Generate()
        {
            return (float)Random.NextDouble();
        }
    }
}
