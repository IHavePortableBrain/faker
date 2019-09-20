using Faker.Generators.System;
using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Faker
{
    public class Faker : IFaker
    {
        protected Dictionary<Type, IGenerator> GeneratorByType;
        protected readonly Random Random = new Random();

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected object Create(Type type)
        {
            object created = null;

            if (GeneratorByType.TryGetValue(type, out IGenerator generator)){
                created = generator.Generate(Random);
            }
            else if (type.IsEnum)
            {
            }
            else if (type.IsClass)
            {
                int longestConstructorParamListLength = 0;
                ConstructorInfo constructorToUseInfo = null;

                foreach (ConstructorInfo constructor in type.GetConstructors())
                {
                    if (constructor.GetParameters().Length > longestConstructorParamListLength)
                    {
                        longestConstructorParamListLength = constructor.GetParameters().Length;
                        constructorToUseInfo = constructor;
                    }
                }

                created = constructorToUseInfo == null ? CreateByProperties(type) : CreateByConstructor(type, constructorToUseInfo);

            }
            else if (type.IsValueType)
            {
                created = Activator.CreateInstance(type);
            }

            return created;
        }

        private object CreateByConstructor(Type type, ConstructorInfo constructorInfo)
        {
            var parametersValues = new List<object>();
            object created;

            foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
            {
                object value = Create(parameterInfo.ParameterType);
                parametersValues.Add(value);
            }
            try
            {
                created = constructorInfo.Invoke(parametersValues.ToArray());
                return created;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private object CreateByProperties(Type type)
        {
            object created = Activator.CreateInstance(type);
            object value;

            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                value = Create(fieldInfo.FieldType);
                fieldInfo.SetValue(created, value);
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    value = Create(propertyInfo.PropertyType);
                    propertyInfo.SetValue(created, value);
                }
            }

            return created;
        }

        public Faker()
        {
            GeneratorByType = new Dictionary<Type, IGenerator>();

            Assembly generatorsAssembly = Assembly.GetAssembly(typeof(IGenerator));
            foreach(Type type in generatorsAssembly.DefinedTypes)
            {
                if (typeof(IGenerator).IsAssignableFrom(type) && type.IsClass)
                {
                    ConstructorInfo[] constructorsInfo = type.GetConstructors();
                    IGenerator generator = (IGenerator)constructorsInfo[0].Invoke(new object[0]); // relying on fact that there is only one ctor in each generator.system class with no parametrs
                    GeneratorByType.Add(generator.TypeOfGenerated, generator);
                }
            }
        }

    }
}
