using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class DateTimeGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(DateTime);

        public object Generate(Random random)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            long ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % (DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks)) + DateTime.MinValue.Ticks);
        }
    }
}
