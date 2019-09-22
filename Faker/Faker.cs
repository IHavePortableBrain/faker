using Faker.Generators.System;
using Faker.Generators;
using Faker.Generators.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace Faker
{
    //Todo: rewrite to have unsetted by ctor properties setted. RewritePropertiesAndFieldsWithDefaultValues. 
    //after ctor method identical to create should be called but with comparison of left value of assignment operator(in wich method are called)and its default value
    //many interface creation
    //generators Random, Delegate parametrs should be in constructor
    public class Faker : IFaker
    {
        protected Dictionary<Type, IGenerator> GeneratorByType;
        protected Dictionary<Type, IGenericGenerator> GenericGeneratorByType;
        protected readonly Random Random = new Random();
        protected Stack<Type> TypesNotCreatedYet = new Stack<Type>();

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        protected object Create(Type type)
        {
            object created = null;
            var interfaces = type.GetInterfaces();

            if (GeneratorByType.TryGetValue(type, out IGenerator generator)){
                created = generator.Generate(type);
            }
            else if (type.IsEnum)
            {
                var enumValues = type.GetEnumValues();
                created = enumValues.GetValue(Random.Next() % enumValues.Length);
            }
            else if (type.IsArray)
            {
                ArrayGenerator arrayGenerator = new ArrayGenerator(Random, Create);
                created = arrayGenerator.Generate(type);
            }
            else if (type.IsValueType && type.IsSealed && !type.IsClass && !type.IsPrimitive && !type.IsContextful)//struct 
            {
                created = CreateByProperties(type);
            }
            else if (interfaces.Length > 0)//must be after is Enum to avoid enumerable interface creation attempt
            {
                foreach (Type currInterface in interfaces)
                {
                    Type toSearchInterface = currInterface.IsGenericType ? currInterface.GetGenericTypeDefinition() : currInterface;
                    if (GenericGeneratorByType.TryGetValue(toSearchInterface, out IGenericGenerator genericGenerator))
                    {
                        created = genericGenerator.Generate(type);
                        break;
                    }
                }
            }
            else if (type.IsClass && !type.IsAbstract)
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
                try
                {
                    created = constructorToUseInfo == null ? CreateByProperties(type) : CreateByConstructor(type, constructorToUseInfo);
                }
                catch (Exception e)
                {
                    created = null;
                }
            }
            else if (type.IsValueType)
            {
                created = Activator.CreateInstance(type);
            }
            
            return created;
        }

        private object CreateByConstructor(Type type, ConstructorInfo constructorInfo)
        {
            if (TypesNotCreatedYet.Contains(type))
                return null;
            else
                TypesNotCreatedYet.Push(type);

            object created;
            var parametersValues = new List<object>();
            

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
            finally
            {
                TypesNotCreatedYet.Pop();
            }
        }

        private object CreateByProperties(Type type)
        {
            if (TypesNotCreatedYet.Contains(type))
                return null;
            else
                TypesNotCreatedYet.Push(type);

            object created = Activator.CreateInstance(type);
            object value;
            try
            {
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
            }
            finally
            {
                TypesNotCreatedYet.Pop();
            }
            return created;
        }

        public Faker()
        {
            GeneratorByType = new Dictionary<Type, IGenerator>();
            GenericGeneratorByType = new Dictionary<Type, IGenericGenerator>();

            Assembly generatorsAssembly = Assembly.GetAssembly(typeof(IGenerator));//and IGenericGenerator
            foreach(Type type in generatorsAssembly.DefinedTypes)
            {
                if (typeof(IGenericGenerator).IsAssignableFrom(type) && type.IsClass) // first check classes wich inherit IGenerator
                {
                    ConstructorInfo[] constructorsInfo = type.GetConstructors();
                    GenerateAnyTypeDelegate generateAnyTypeDelegate = Create;
                    IGenericGenerator genericGenerator = (IGenericGenerator)constructorsInfo[0].Invoke(new object[] { Random, generateAnyTypeDelegate }); 
                    GenericGeneratorByType.Add(genericGenerator.TypeOfGenerated, genericGenerator);
                }
                else if(typeof(IGenerator).IsAssignableFrom(type) && type.IsClass)
                {
                    ConstructorInfo[] constructorsInfo = type.GetConstructors();
                    IGenerator generator = (IGenerator)constructorsInfo[0].Invoke(new object[] { Random });
                    GeneratorByType.Add(generator.TypeOfGenerated, generator);
                }
            }
        }
    }
}
