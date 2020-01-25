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
                Assert.AreEqual(result, $" {kv.Value}");
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



    }
}