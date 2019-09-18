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
        protected Random Random = new Random();

        public object Generate()
        {
            return (long)Random.NextDouble();
        }
    }
}
