using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class SByteGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(sbyte);
        

        public object Generate(Random random)
        {
            return (sbyte)random.Next();
        }
    }
}
