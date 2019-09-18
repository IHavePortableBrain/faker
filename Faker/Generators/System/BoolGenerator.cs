using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.System
{
    public class BoolGenerator : IGenerator
    {
        public Type TypeOfGenerated => typeof(bool);

        public object Generate()
        {
            return (bool)true;
        }
    }
}
