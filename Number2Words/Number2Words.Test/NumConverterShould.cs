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


    }
}