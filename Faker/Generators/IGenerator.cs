﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators
{
    public interface IGenerator
    {
        object Generate(Random random);
        Type TypeOfGenerated { get; }
    }
}
