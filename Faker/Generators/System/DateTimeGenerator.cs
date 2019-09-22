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
        public Random Random { get; private set; }

        public DateTimeGenerator(Random random)
        {
            Random = random;
        }

        public object Generate(Type t)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            long ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % (DateTime.MaxValue.Ticks - DateTime.MinValue.Ticks)) + DateTime.MinValue.Ticks);
        }
    }
}
