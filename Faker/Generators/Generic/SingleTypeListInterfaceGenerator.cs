using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.Generic
{
    //TODO: rewrite to icollection ad its the most general Generic interface
    public class SingleTypeListInterfaceGenerator : IGenericGenerator
    {
        protected readonly int MaxGenericLength = 5;
        protected readonly int MinGenericLength = 1;
        public Type TypeOfGenerated => typeof(IList<>);
        public Random Random { get; private set; }
        public GenerateAnyTypeDelegate GenerateAnyTypeDelegate { get; private set; }

        public SingleTypeListInterfaceGenerator(Random random, GenerateAnyTypeDelegate generateAnyTypeDelegate)
        {
            Random = random;
            GenerateAnyTypeDelegate = generateAnyTypeDelegate;
        }

        public object Generate(Type type)
        {
            Type listElementType = type.GetGenericArguments()[0];
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listElementType));

            int listLength = Random.Next() % MaxGenericLength + MinGenericLength;

            for (int i = 0; i < listLength; i++)
            {
                object listElement = GenerateAnyTypeDelegate.Invoke(listElementType);
                result.Add(listElement);
            }
            return result;
        }
    }
}
