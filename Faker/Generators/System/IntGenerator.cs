using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class IntGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(int);
        protected Random Random = new Random();

        public object Generate()
        {
            return Random.Next();
        }
    }
}
