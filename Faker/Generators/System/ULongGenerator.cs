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
        

        public object Generate(Random random)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }
    }
}
