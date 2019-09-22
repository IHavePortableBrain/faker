using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Test
{
    [TestClass]
    public class FakerTester
    {
        private readonly Faker _faker = new Faker();

        [TestMethod] 
        //no ctor with param list expected
        public void DTOwithNoInnerDTOCreationTest()
        {
            DTClassWithNoInnerDTClasses dto = _faker.Create<DTClassWithNoInnerDTClasses>();
            
            Assert.AreNotEqual(dto.booll, default(bool));
            Assert.AreNotEqual(dto.bytee, default(byte));
            Assert.AreNotEqual(dto.DateTime, default(DateTime));
            Assert.AreNotEqual(dto.deci, default(decimal));
            Assert.AreNotEqual(dto.doubl, default(double));
            Assert.AreNotEqual(dto.floatt, default(float));
            Assert.AreNotEqual(dto.longg, default(long));
            Assert.AreNotEqual(dto.str, default(string));
            Assert.AreNotEqual(dto.ulongg, default(ulong));
            Assert.AreNotEqual(dto.ushortt, default(ushort));
            Assert.AreNotEqual(dto.uintt, default(uint));
        }

        [TestMethod]
        public void DTOwithInnerDTOCreationTest()
        {
            DTClassWithInnerDTClasses dto = _faker.Create<DTClassWithInnerDTClasses>();
            Assert.AreNotEqual(dto.DTONoInner, default(DTClassWithNoInnerDTClasses));
            Assert.AreNotEqual(dto.DTONoInner.deci, default(decimal));
            Assert.AreNotEqual(dto.str, default(string));
        }

        [TestMethod]
        public void DTOCreationByCtorTest()
        {
            DTClassParameterizedCtor dto = _faker.Create<DTClassParameterizedCtor>();
            Assert.AreNotEqual(dto.longg, default(long));
            Assert.AreEqual(dto.DateTime, default(DateTime));
            Assert.AreNotEqual(dto.str, default(string));
        }

        [TestMethod]
        public void DTOCreationByPropertyTest()
        {
            DTClassNoParameterizedCtor dto = _faker.Create<DTClassNoParameterizedCtor>();
            Assert.AreEqual(dto.longg, default(long));
            Assert.AreNotEqual(dto.DateTime, default(DateTime));
            Assert.AreEqual(dto.str, default(string));
        }

        [TestMethod]
        public void ComplexCtorClassCreationTest()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = _faker.Create<System.Xml.Serialization.XmlSerializer>();
            Assert.AreEqual(xmlSerializer, null);
        }

        [TestMethod]
        public void ListCreationTest()
        {
            FoosList foosList = _faker.Create<FoosList>();
            Assert.IsNotNull(foosList.Foos[0]);
            Assert.AreNotEqual(default(DateTime), foosList.DateTime);
            Assert.IsNull(foosList.XmlSerializer);
            Assert.AreNotEqual(default(int), foosList.Foos[0].myStruct.a);
            Assert.AreNotEqual(default(string), foosList.Foos[0].str);
        }

        [TestMethod]
        public void ArrayCreationTest()
        {
            FooArray fooArray = _faker.Create<FooArray>();
            Assert.IsNotNull(fooArray);
            Assert.IsNotNull(fooArray.Foos);
            Assert.IsNotNull(fooArray.Foos[0]);
            Assert.AreNotEqual(default(DateTime), fooArray.DateTime);
            Assert.AreNotEqual(default(int), fooArray.Foos[0].myStruct.a);
            Assert.AreNotEqual(default(string), fooArray.Foos[0].str);
        }

    }
}
