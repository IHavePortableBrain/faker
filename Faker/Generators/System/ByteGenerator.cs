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
        protected Random Random = new Random();

        public object Generate()
        {
            return (byte)Random.Next();
        }
    }
}
