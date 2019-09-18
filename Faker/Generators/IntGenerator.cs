using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators
{
    public class IntGenerator : IGenerator
    {
        Type IGenerator.TypeOfGenerated => typeof(int);
        private Random _random = new Random();

        public object Generate()
        {
            return _random.Next();
        }
    }
}
