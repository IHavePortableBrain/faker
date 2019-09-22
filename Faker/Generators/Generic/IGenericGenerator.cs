using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.Generic
{
    public delegate object GenerateAnyTypeDelegate(Type type);

    public interface IGenericGenerator:IGenerator
    {
        GenerateAnyTypeDelegate GenerateAnyTypeDelegate { get; }
    }
}
