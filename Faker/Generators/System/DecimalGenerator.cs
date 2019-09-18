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
        protected Random Random = new Random();

        public object Generate()
        {
            return (decimal)Random.NextDouble();
        }
    }
}
