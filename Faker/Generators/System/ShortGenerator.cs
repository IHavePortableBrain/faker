using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ShortGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(short);
        

        public object Generate(Random random)
        {
            return (short)random.Next();
        }
    }
}
