using NUnit.Framework;
using Moq;
using Number2Words;
using System.Collections.Generic;
using System;

namespace Number2Words.Test
{
    public class NumConverterShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PassInstanceTest()
        {
            var sut = new NumConverter();
            Assert.IsInstanceOf<NumConverter>(sut);
        }
        [Test]
        public void InstantiateResultDictionary()
        {
            var sut = new NumConverter();
            Assert.IsNotNull(sut._resultDictionary);
        }
        [Test]
        public void GetLessTwentyThrowAcceptOnlyLessThanTwenty()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("AcceptOnlyLessThanTwentys"), () =>
            {
                var sut = new NumConverter();
                sut.GetLessTwenty(21);
            });
        }

        [Test]
        public void GetLessTwentyReturnsValue()
        {
            Dictionary<uint, string> lessTwenty = new Dictionary<uint, string>();
            lessTwenty.Add(0, "zero");
            lessTwenty.Add(1, "one");
            lessTwenty.Add(2, "two");
            lessTwenty.Add(3, "three");
            lessTwenty.Add(4, "four");
            lessTwenty.Add(5, "five");
            lessTwenty.Add(6, "six");
            lessTwenty.Add(7, "seven");
            lessTwenty.Add(8, "eight");
            lessTwenty.Add(9, "nine");
            lessTwenty.Add(10, "ten");

            lessTwenty.Add(11, "eleven");
            lessTwenty.Add(12, "twelve");
            lessTwenty.Add(13, "thirteen");
            lessTwenty.Add(14, "fourteen");
            lessTwenty.Add(15, "fifteen");
            lessTwenty.Add(16, "sixteen");
            lessTwenty.Add(17, "seventeen");
            lessTwenty.Add(18, "eighteen");
            lessTwenty.Add(19, "nineteen");
            var sut = new NumConverter();
            foreach(var kv in lessTwenty)
            {
                string result = sut.GetLessTwenty(kv.Key);
                Assert.AreEqual($" {kv.Value}", result);
            }

        }

        [Test]
        public void GetLessTwentyReturnsValueWithPrefix()
        {
            Dictionary<uint, string> lessTwenty = new Dictionary<uint, string>();
            lessTwenty.Add(0, "and zero");
            lessTwenty.Add(1, "and one");
            lessTwenty.Add(2, "and two");
            lessTwenty.Add(3, "and three");
            lessTwenty.Add(4, "and four");
            lessTwenty.Add(5, "and five");
            lessTwenty.Add(6, "and six");
            lessTwenty.Add(7, "and seven");
            lessTwenty.Add(8, "and eight");
            lessTwenty.Add(9, "and nine");
            lessTwenty.Add(10, "and ten");

            lessTwenty.Add(11, "and eleven");
            lessTwenty.Add(12, "and twelve");
            lessTwenty.Add(13, "and thirteen");
            lessTwenty.Add(14, "and fourteen");
            lessTwenty.Add(15, "and fifteen");
            lessTwenty.Add(16, "and sixteen");
            lessTwenty.Add(17, "and seventeen");
            lessTwenty.Add(18, "and eighteen");
            lessTwenty.Add(19, "and nineteen");
            var sut = new NumConverter();
            foreach (var kv in lessTwenty)
            {
                string result = sut.GetLessTwenty(kv.Key, "and");
                Assert.AreEqual($"{kv.Value}", result);
            }

        }

        [Test]
        public void GetLessTensThrowAcceptOnlyTens()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("AcceptOnlyTens"), () =>
            {
                var sut = new NumConverter();
                sut.GetTens(100);
            });
        }
        [Test]
        public void GetTensReturnsValue()
        {
            Dictionary<uint, string> tensDictionary = new Dictionary<uint, string>();
            tensDictionary.Add(20, "twenty");
            tensDictionary.Add(30, "thirty");
            tensDictionary.Add(40, "forty");
            tensDictionary.Add(50, "fifty");
            tensDictionary.Add(60, "sixty");
            tensDictionary.Add(70, "seventy");
            tensDictionary.Add(80, "eighty");
            tensDictionary.Add(90, "ninety");
            var sut = new NumConverter();
            foreach (var kv in tensDictionary)
            {
                string result = sut.GetTens(kv.Key);
                Assert.AreEqual($" {kv.Value}", result);
            }
        }
        [Test]
        public void GetTensReturnsValueWithPrefix()
        {
            Dictionary<uint, string> tensDictionary = new Dictionary<uint, string>();
            tensDictionary.Add(20, "and twenty");
            tensDictionary.Add(30, "and thirty");
            tensDictionary.Add(40, "and forty");
            tensDictionary.Add(50, "and fifty");
            tensDictionary.Add(60, "and sixty");
            tensDictionary.Add(70, "and seventy");
            tensDictionary.Add(80, "and eighty");
            tensDictionary.Add(90, "and ninety");
            var sut = new NumConverter();
            foreach (var kv in tensDictionary)
            {
                string result = sut.GetTens(kv.Key, "and");
                Assert.AreEqual($"{kv.Value}", result);
            }
        }
        [Test]
        public void GetTensCallsGetLessTwentyWhenLower()
        {

            var MockSUT = new Mock<NumConverter>() { CallBase = true };
            MockSUT.Object.GetTens(18, "");
            MockSUT.Verify(x => x.GetLessTwenty(18, ""), Times.Once);

        }



        [Test]
        public void GetHundredCallsGetTensWhenLower()
        {
            
            var MockSUT = new Mock<NumConverter>();
            MockSUT.Object.GetHundred(99, "");
            MockSUT.Verify(x => x.GetTens(99, ""), Times.AtLeastOnce);
            
        }

        [Test]
        public void GetHundredThrowAcceptOnlyHundreds()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("AcceptOnlyHundreds"), () =>
            {
                var sut = new NumConverter();
                sut.GetHundred(1001);
            });
        }
        [Test]
        public void GetHundredReturnsValue()
        {
            var sut = new NumConverter();
            string result = sut.GetHundred(300);
            Assert.AreEqual(" three hundred ", result);
        }

        [Test]
        public void GetHundredReturnsValueWithPrefix()
        {
            var sut = new NumConverter();
            string result = sut.GetHundred(301, "and");
            Assert.AreEqual(" three hundred and one", result);
        }

        [Test]
        public void NumToWordsThrowsArgumentException()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("NotANumber"), () =>
            {
                var sut = new NumConverter();
                sut.NumToWords("abc");
            });
        }

        [Test]
        public void NumToWordsReturnsValueWhole()
        {
            var sut = new NumConverter();
            string result = sut.NumToWords(new decimal(1000000));
            Assert.AreEqual("one million", result);
        }


        [Test]
        public void NumToWordsReturnsValueWholeCharacteristicOnly()
        {
            var sut = new NumConverter();
            string result = sut.NumToWords(new decimal(1854600));
            Assert.AreEqual("one million eight hundred and fifty four thousand six hundred", result);
        }

        [Test]
        public void NumToWordsReturnsValueWholeWithMantissa()
        {
            var sut = new NumConverter();
            string result = sut.NumToWords(new decimal(1854600.23));
            Assert.AreEqual("one million eight hundred and fifty four thousand six hundred and twenty three cents", result);
        }

        [Test]
        public void NumToWordsReturnsValueWholeMantissaOnly()
        {
            var sut = new NumConverter();
            string result = sut.NumToWords(new decimal(0.23));
            Assert.AreEqual("zero and twenty three cents", result);
        }

        [Test]
        public void NumToWordsThrowsArgumentExceptionBigPrecision()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Property("Message").EqualTo("AcceptOnlyTens"), () =>
            {
                var sut = new NumConverter();
                string result = sut.NumToWords(new decimal(0.2366));
            });
        }





    }
}