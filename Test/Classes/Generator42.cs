using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class Generator42 : IGenerator
    {
        public Random Random => Random;

        public Type TypeOfGenerated => typeof(Int32);

        public object Generate(Type type)
        {
            return (Int32)42;
        }
    }
}
