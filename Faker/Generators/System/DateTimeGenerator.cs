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
        protected Random Random = new Random();

        public object Generate()
        {
            return new DateTime((long)Random.NextDouble());
        }
    }
}
