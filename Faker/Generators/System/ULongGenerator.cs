using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ULongGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(ulong);
        protected Random Random = new Random();

        public object Generate()
        {
            return (ulong)Random.NextDouble();
        }
    }
}
