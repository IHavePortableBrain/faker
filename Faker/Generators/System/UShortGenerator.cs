using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class UshortGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(ushort);
        protected Random Random = new Random();

        public object Generate()
        {
            return (ushort)Random.Next();
        }
    }
}
