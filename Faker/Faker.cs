using Faker.Generators.System;
using Faker.Generators;
using Faker.Generators.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Faker.Config;
using System.Runtime.InteropServices;

namespace Faker
{
    //Todo: 1)rewrite to have unsetted by ctor properties setted. RewritePropertiesAndFieldsWithDefaultValues. 
    //after ctor method identical to create should be called but with comparison of left value of assignment operator(in wich method are called)and its default value
    //2)many interface creation
    //3)MSBuild, fix solution hierarchy/dll folder path
    //4) rewrite config with MemberInfo?
    public class Faker : IFaker
    {
        protected Dictionary<Type, IGenerator> GeneratorByType;
        protected Dictionary<Type, IGenericGenerator> GenericGeneratorByType;
        protected Dictionary<FieldInfo, IGenerator> CustomGeneratorByFieldInfo;
        protected Dictionary<PropertyInfo, IGenerator> CustomGeneratorByPropInfo;
        protected readonly Random Random = new Random();
        protected Stack<Type> TypesNotCreatedYet = new Stack<Type>();
        private readonly string _pluginsPath = "Plugin\\netstandard2.0";


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

        protected bool TryCreateByCustomGenerator(FieldInfo fieldInfo, out object generated)
        {
            if (CustomGeneratorByFieldInfo.TryGetValue(fieldInfo, out IGenerator generator))
            {
                generated = generator.Generate(fieldInfo.FieldType);
                return true;
            }
            else
            {
                generated = default(object);
                return false;
            }
        }

        protected bool TryCreateByCustomGenerator(PropertyInfo propertyInfo, out object generated)
        {
            if (CustomGeneratorByPropInfo.TryGetValue(propertyInfo, out IGenerator generator))
            {
                generated = generator.Generate(propertyInfo.PropertyType);
                return true;
            }
            else
            {
                generated = default(object);
                return false;
            }
        }

        protected bool TryAssignByCustomGenerator(Type classToBeConstructed, ParameterInfo parameterToBeAssignedInfo, out object value)
        {
            value = null;
            string paramNameToMemberName = Char.ToUpper(parameterToBeAssignedInfo.Name[0]) + parameterToBeAssignedInfo.Name.Substring(1);

            PropertyInfo propInfo = classToBeConstructed.GetProperty(paramNameToMemberName);
            FieldInfo fieldInfo = classToBeConstructed.GetField(paramNameToMemberName);
            if (propInfo != null)
            {
                if (CustomGeneratorByPropInfo.TryGetValue(propInfo, out IGenerator generator))
                {
                    value = generator.Generate(propInfo.PropertyType);
                    return true;
                }
            }
            if (fieldInfo != null)
            {
                if (CustomGeneratorByFieldInfo.TryGetValue(fieldInfo, out IGenerator generator))
                {
                    value = generator.Generate(fieldInfo.FieldType);
                    return true;
                }
            }

            return false;
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
                if (!TryAssignByCustomGenerator(type, parameterInfo, out object value))
                    value = Create(parameterInfo.ParameterType);
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
                    if (!TryCreateByCustomGenerator(fieldInfo, out value))
                        value = Create(fieldInfo.FieldType);
                    fieldInfo.SetValue(created, value);
                }

                foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                {
                    if (propertyInfo.CanWrite)
                    {
                        if (!TryCreateByCustomGenerator(propertyInfo, out value))
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

        private void AddGenerators(Assembly generatorsAssembly)
        {
            foreach (Type type in generatorsAssembly.DefinedTypes)
            {
                if (typeof(IGenericGenerator).IsAssignableFrom(type) && type.IsClass) // first check classes wich inherit IGenerator
                {
                    
                    ConstructorInfo[] constructorsInfo = type.GetConstructors();
                    GenerateAnyTypeDelegate generateAnyTypeDelegate = Create;
                    IGenericGenerator genericGenerator = (IGenericGenerator)constructorsInfo[0].Invoke(new object[] { Random, generateAnyTypeDelegate });
                    if (GenericGeneratorByType.Keys.Contains(genericGenerator.TypeOfGenerated))
                        continue;
                    else
                        GenericGeneratorByType.Add(genericGenerator.TypeOfGenerated, genericGenerator);
                }
                else if (typeof(IGenerator).IsAssignableFrom(type) && type.IsClass)
                {
                    ConstructorInfo[] constructorsInfo = type.GetConstructors();
                    IGenerator generator = (IGenerator)constructorsInfo[0].Invoke(new object[] { Random });
                    if (GeneratorByType.Keys.Contains(generator.TypeOfGenerated))
                        continue;
                    else
                        GeneratorByType.Add(generator.TypeOfGenerated, generator);
                }
            }
        }

        private void LoadPluginsAndAddGeneratorsFromThem()
        {
            List<Assembly> assemblies = new List<Assembly>();
            try
            {
                foreach (string file in Directory.GetFiles(_pluginsPath, "*.dll"))
                {
                    try
                    {
                        Assembly pluginAssembly = Assembly.LoadFile(new FileInfo(file).FullName);
                        assemblies.Add(pluginAssembly);
                        AddGenerators(pluginAssembly);
                    }
                    catch (BadImageFormatException e)
                    {
                        throw e;
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
        }

        public Faker(IConfig config = null)
        {
            GeneratorByType = new Dictionary<Type, IGenerator>();
            GenericGeneratorByType = new Dictionary<Type, IGenericGenerator>();
            CustomGeneratorByFieldInfo = new Dictionary<FieldInfo, IGenerator>();
            CustomGeneratorByPropInfo = new Dictionary<PropertyInfo, IGenerator>();

            Assembly generatorsAssembly = Assembly.GetAssembly(typeof(IGenerator));//and IGenericGenerator
            AddGenerators(generatorsAssembly);
            LoadPluginsAndAddGeneratorsFromThem();
            if (config != null)
            {
                CustomGeneratorByFieldInfo = config.GeneratorByFieldInfo;
                CustomGeneratorByPropInfo = config.GeneratorByPropInfo;
            }

        }
    }
}
