using System;
using NUnit.Framework;

namespace EasyBuilder.Tests
{
    [TestFixture]
    public class EasyBuilderTests
    {
        [Test]
        public void EasyBuilderShouldReturnInstance()
        {
            DummyObject obj = EasyBuilder.BuildA<DummyObject>();
            Assert.IsInstanceOf<DummyObject>(obj);
        }

        [Test]
        public void EasyBuilderShouldSetPropertyValue()
        {
            DummyObject obj = EasyBuilder.BuildA<DummyObject>().SetProperty(y => y.Property1, "Hello");
            Assert.AreEqual("Hello", obj.Property1);
        }

        [Test]
        public void EasyBuilderShouldSetPropertyValueFromFuncResult()
        {
            DummyObject obj = EasyBuilder.BuildA<DummyObject>().SetProperty(y => y.Property1, () => "Hello");
            Assert.AreEqual("Hello", obj.Property1);
        }

        [Test]
        public void EasyBuilderShouldUseLatestAssignmentPropertyValue()
        {
            DummyObject obj =
                EasyBuilder.BuildA<DummyObject>()
                    .SetProperty(y => y.Property1, "Hello")
                    .SetProperty(y => y.Property1, "Hello2");
            Assert.AreEqual("Hello2", obj.Property1);
        }

        [Test]
        public void EasyBuilderCanAssignMultipleProperties()
        {
            DummyObject obj =
                EasyBuilder.BuildA<DummyObject>()
                    .SetProperty(y => y.Property1, "Hello")
                    .SetProperty(y => y.Property2, 5000);
            Assert.AreEqual("Hello", obj.Property1);
            Assert.AreEqual(5000, obj.Property2);
        }
    }

    public class DummyObject
    {
        public string Property1 { get; set; }

        public int Property2 { get; set; }
    }
}
