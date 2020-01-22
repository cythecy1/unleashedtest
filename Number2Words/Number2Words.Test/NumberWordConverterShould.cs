using NUnit.Framework;
using Moq;
using Number2Words;
using System.Collections.Generic;
using System;

namespace Number2Words.Test
{
    public class NumberWordConverterShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PassInstanceTest()
        {
            var sut = new NumberWordConverter(null, null);
            Assert.IsInstanceOf<NumberWordConverter>(sut);
        }

        [Test]
        public void ThrowErrorOnEmptyNumDictionary()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("NumDictionaryEmpty"),
                delegate {
                    var mockNumDict = new Mock<IDictionary<ulong, string>>().Object;
                    var sut = new NumberWordConverter(mockNumDict, null);
                });
        }


        [Test]
        public void ThrowErrorOnEmptyDigitDictionary()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("DigitDictionaryEmpty"),
                delegate {
                    var mockDigDict = new Mock<IDictionary<ulong, string>>().Object;
                    var sut = new NumberWordConverter(null, mockDigDict);
                });
        }



        [Test]
        public void ProcessQuickReturn()
        {
            NumberWordConverter sut = new NumberWordConverter(null, null);

            string passResult = sut.QuickReturn(20);
            Assert.That(passResult, Is.EqualTo("twenty"));

            string emptyResult = sut.QuickReturn(21);
            Assert.IsEmpty(emptyResult);
        }
    }
}