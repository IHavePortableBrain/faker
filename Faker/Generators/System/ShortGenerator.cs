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
        protected Random Random = new Random();

        public object Generate()
        {
            return (short)Random.Next();
        }
    }
}
