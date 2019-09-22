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
        public Random Random { get; private set; }

        public UshortGenerator(Random random)
        {
            Random = random;       
        } 

        public object Generate(Type t)
        {
            return (ushort)Random.Next();
        }
    }
}
