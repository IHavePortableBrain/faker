using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class ByteGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(byte);
        

        public object Generate(Random random)
        {
            return (byte)random.Next();
        }
    }
}
