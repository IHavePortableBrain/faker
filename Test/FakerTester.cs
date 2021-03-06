﻿using System;
using Faker.Config;
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

        [TestMethod]
        public void NotImplementedBaseTypeGenerationTest()
        {
            NotImplementedTypeGeneratorFieldAndPropClass notImplemented = _faker.Create<NotImplementedTypeGeneratorFieldAndPropClass>();
            Assert.AreEqual(default(char), notImplemented.Charr);
            Assert.AreEqual(default(char), notImplemented.CharProp);
        }

        [TestMethod]
        public void RecurrentDependenceTest()
        {
            RecurrentDependent recc = _faker.Create<RecurrentDependent>();
            Type recType = typeof(RecurrentDependent);
            Type recType2 = typeof(RecurrentDependent2);
            Assert.IsNull(recc.RecurrentProperty);
            Assert.AreNotEqual(default(int), recc.Intt);
            Assert.IsNotNull(recc.Recurrent2);
            Assert.IsNull(recc.Recurrent2.Recurrent);
            Assert.IsNull(recc.Recurrent2.Recurrent2);
            Assert.IsNull(recc.Recurrent2.RecurrentProperty);
            Assert.AreNotEqual(default(int), recc.Recurrent2.Intt);
        }

        [TestMethod]
        public void ConfigCreationWithCtorTest()
        {
            Faker _faker = new Faker();

            IConfig config = new FakerConfig();
            config.Add<QuestionWithCtor, int, Generator42>(Question => Question.Answer);
            var faker = new Faker(config);
            QuestionWithCtor q = faker.Create<QuestionWithCtor>();
            Assert.AreEqual(42, q.Answer);
            Assert.AreEqual(default(int), q.UnsettedAnswer);
            Assert.AreEqual(default(int), q.AnswerField);
        }

        [TestMethod]
        public void ConfigCreationWithNoCtorTest()
        {
            Faker _faker = new Faker();

            IConfig config = new FakerConfig();
            config.Add<QuestionWithNoCtor, int, Generator42>(Question => Question.Answer);
            config.Add<QuestionWithNoCtor, string, GeneratorMambo>(Question => Question.Mambo);
            var faker = new Faker(config);
            QuestionWithNoCtor q = faker.Create<QuestionWithNoCtor>();
            Assert.AreEqual(42, q.Answer);
            Assert.AreEqual(default(int), q.UnsettedAnswer);
            Assert.AreEqual("Mambo", q.Mambo);
            Assert.AreNotEqual(42, q.NoAnswerField);
        }

    }
}
