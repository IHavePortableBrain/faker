using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Faker;
using Faker.Config;
using Faker.Generators;

namespace Faker.UseExample
{
    public struct MyStruct
    {
        public int a;
        public decimal b;
    }

    public enum MyEnum
    {
        e0, e1, e2, e3, e4, e5
    }

    public class Foo
    {
        public MyEnum myEnum;
        public MyStruct myStruct;
        private long l;
        sbyte sb;
        private string str;


        public Foo(int i, string str)
        {
            this.l = i;
            this.str = str;
        }
    }

    public class Bar
    {
        public long l;
        public DateTime DateTime;
        public List<Foo> foos;
        public System.Xml.Serialization.CodeExporter CodeExporter;
        public System.Xml.Serialization.XmlSerializer XmlSerializer;

    }

    public class Question
    {
        public int Answer { get; }

        public Question(int answer)
        {
            Answer = answer;
        }
    }

    public class Generator42 : IGenerator
    {
        public Random Random => Random;

        public Type TypeOfGenerated => typeof(Int32);

        public object Generate(Type type)
        {
            return (Int32)42;
        }
    }

    class UseExample
    {
        static void Main(string[] args)
        {
            Faker _faker = new Faker();
            Bar bar = _faker.Create<Bar>();
            FooArray fooArray = _faker.Create<FooArray>();
            decimal[] decArr = _faker.Create<decimal[]> ();
            FileStream fs = _faker.Create<FileStream>();

            IConfig config = new FakerConfig();
            config.Add<Question, int, Generator42>(Question => Question.Answer);
            var faker = new Faker(config);
            Question q = faker.Create<Question>();
            Console.ReadKey();
        }
    }
}
