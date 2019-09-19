using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class LongGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(long);
        

        public object Generate(Random random)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }
    }
}
