using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Generators.Generic
{
    public class ArrayGenerator:IGenericGenerator
    {
        public readonly int MaxArrayLength = 5;
        public readonly int MinArrayLength = 1;
        public Type TypeOfGenerated => typeof(Array);
        public Random Random { get; private set; }
        public GenerateAnyTypeDelegate GenerateAnyTypeDelegate { get; private set; }

        public ArrayGenerator(Random random, GenerateAnyTypeDelegate generateAnyTypeDelegate)
        {
            Random = random;
            GenerateAnyTypeDelegate = generateAnyTypeDelegate;
        }

        public object Generate(Type type)
        {
            Type arrayElementType = type.GetElementType();
            int arrLength = Random.Next() % MaxArrayLength + MinArrayLength;
            Array result = Array.CreateInstance(arrayElementType, arrLength);

            for (int i = 0; i < arrLength; i++)
            {
                object arrayElement = GenerateAnyTypeDelegate.Invoke(arrayElementType);
                result.SetValue(arrayElement, i);
            }
            return result;
        }
    }
}
