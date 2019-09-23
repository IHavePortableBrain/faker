using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class GeneratorMambo : IGenerator
    {
        public Random Random => Random;

        public Type TypeOfGenerated => typeof(string);

        public object Generate(Type type)
        {
            return "Mambo";
        }
    }
}
