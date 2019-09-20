using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Faker.Test
{
    public class FoosList
    {
        public long l;
        public DateTime DateTime;
        public List<Foo> Foos;
        public CodeExporter CodeExporter;
        public XmlSerializer XmlSerializer;

        public FoosList(List<Foo> foos, XmlSerializer xmlSerializer, DateTime dateTime)
        {
            Foos = foos;
            XmlSerializer = xmlSerializer;
            DateTime = dateTime;
        }
    }
}
