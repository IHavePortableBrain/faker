using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class UIntGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(uint);
        protected Random Random = new Random();

        public object Generate()
        {
            return (uint)Random.NextDouble();
        }
    }
}
