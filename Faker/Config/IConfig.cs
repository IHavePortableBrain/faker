using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Config
{
    public interface IConfig
    {
        void Add<TClass, TClassMember, TGenerator>(Expression<Func<TClass, TClassMember>> expression)
            where TClass : class
            where TGenerator : IGenerator, new();

        Dictionary<FieldInfo, IGenerator> GeneratorByFieldInfo { get; }
        Dictionary<PropertyInfo, IGenerator> GeneratorByPropInfo { get; }
    }
}
