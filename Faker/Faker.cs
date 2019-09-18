using Faker.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected void Test()
        {
            Console.WriteLine("hoho");
        }

        public Faker()
        {
            GeneratorByType = new Dictionary<Type, IGenerator>();

            IGenerator generator = new IntGenerator();
            GeneratorByType.Add(generator.TypeOfGenerated, generator);
        }
    }
}
