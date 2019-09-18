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

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected object Create(Type type)
        {
            object created = null;

            if (GeneratorByType.TryGetValue(type, out IGenerator generator)){
                created = generator.Generate();
            }
            else
            {
                throw new KeyNotFoundException();
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
