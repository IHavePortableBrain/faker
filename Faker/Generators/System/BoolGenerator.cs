using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class BoolGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(bool);
        public Random Random { get; private set; }

        public BoolGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            return (bool)true;
        }
    }
}
