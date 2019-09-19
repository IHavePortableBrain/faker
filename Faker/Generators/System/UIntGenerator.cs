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
        

        public object Generate(Random random)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
    }
}
