using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class IntGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(int);
        

        public object Generate(Random random)
        {
            return random.Next();
        }
    }
}
