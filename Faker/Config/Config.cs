using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Faker.Generators;

namespace Faker.Config
{
    public class FakerConfig:IConfig
    {
        public Dictionary<FieldInfo, IGenerator> GeneratorByFieldInfo { get; private set; }
        public Dictionary<PropertyInfo, IGenerator> GeneratorByPropInfo { get; private set; }

        public void Add<TClass, TClassMember, TGenerator>(Expression<Func<TClass, TClassMember>> expression)
            where TClass : class
            where TGenerator : IGenerator, new()
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Invalid expression");
            }
            IGenerator generator = (IGenerator)Activator.CreateInstance(typeof(TGenerator));
            if (!generator.TypeOfGenerated.Equals(expression.ReturnType))
            {
                throw new ArgumentException("Invalid generator");
            }
            if (((MemberExpression)expression.Body).Member.MemberType == MemberTypes.Field)
                GeneratorByFieldInfo.Add((FieldInfo)((MemberExpression)expression.Body).Member, generator);
            else   
                GeneratorByPropInfo.Add((PropertyInfo)((MemberExpression)expression.Body).Member, generator);
        }

        public FakerConfig()
        {
            GeneratorByFieldInfo = new Dictionary<FieldInfo, IGenerator>();
            GeneratorByPropInfo = new Dictionary<PropertyInfo, IGenerator>();
        }
    }
}
