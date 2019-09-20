using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.Generic
{
    public class ListInterfaceGenerator : IGenericGenerator
    {
        public readonly int MaxGenericLength = 5;
        public readonly int MinGenericLength = 1;
        public Type TypeOfGenerated => typeof(IList<>);

        public object Generate(Random random, Type type, GenerateAnyTypeDelegate generateAnyTypeDelegate)
        {
            Type listElementType = type.GetGenericArguments()[0];
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listElementType));

            int listSize = random.Next() % MaxGenericLength + MinGenericLength;

            for (int i = 0; i < listSize; i++)
            {
                object listElement = generateAnyTypeDelegate.Invoke(listElementType);
                result.Add(listElement);
            }
            return result;
        }
    }
}
