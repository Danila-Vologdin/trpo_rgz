using System;
using System.Runtime.CompilerServices;
using trpo_rgz;
using Xunit;

namespace Polynom.Tests
{
    public class TMemberTests
    {
        [Fact]
        public void CanGetPower()
        {
            var member = new TMember(5, 4);
            Assert.Equal(4, member.GetPower());
        }

        [Fact]
        public void CanSetPower()
        {
            var member = new TMember(5, 4);
            member.SetPower(10);
            Assert.Equal(10, member.GetPower());
        }

        [Fact]
        public void CanGetCoef()
        {
            var member = new TMember(5, 4);
            Assert.Equal(5, member.GetCoef());
        }

        [Fact]
        public void CanSetCoef()
        {
            var member = new TMember(5, 4);
            member.SetCoef(3);
            Assert.Equal(3, member.GetCoef());
        }

        [Theory]
        [InlineData(true, 5, 4, 5, 4)]
        [InlineData(false, 5, 4, 3, 0)]
        [InlineData(true, 0, 0, 0, 0)]
        public void CanCompare(bool expected, int coef1, int power1,
            int coef2, int power2)
        {
            var member1 = new TMember(coef1, power1);
            var member2 = new TMember(coef2, power2);
            Assert.Equal(expected, member1.IsEqual(member2));
        }

        [Theory]
        [InlineData(5, 4)]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(0, 0)]
        public void CanDiffPower(int coef, int power)
        {
            var member1 = new TMember(coef, power);
            member1 = member1.Diff();
            var member2 = new TMember(coef * power, power - 1);
            Assert.True(member1.IsEqual(member2));
        }

        [Theory]
        [InlineData("5x^4", 5, 4)]
        [InlineData("0x^1",0, 1)]
        [InlineData("5", 5, 0)]
        public void CanConvertToString(string expected,
            int coef, int power)
        {
            var member = new TMember(coef, power);
            Assert.Equal(expected, member.ToString());
        }

        [Fact]
        public void CanCalculate()
        {
            var member = new TMember(3, 2);
            Assert.Equal(12, member.Calculate(2));
        }

        [Fact]
        public void CanAdd()
        {
            var member1 = new TMember(3, 2);
            var member2 = new TMember(4, 2);
            var expected = new TMember(7, 2);
            Assert.True(expected.IsEqual(member1 + member2));
        }

        [Fact]
        public void CanSubtract()
        {
            var member1 = new TMember(3, 2);
            var member2 = new TMember(4, 2);
            var expected = new TMember(-1, 2);
            Assert.True(expected.IsEqual(member1 - member2));
        }

        [Fact]
        public void CanMultiply()
        {
            var member1 = new TMember(3, 2);
            var member2 = new TMember(4, 3);
            var expected = new TMember(12, 5);
            Assert.True(expected.IsEqual(member1 * member2));
        }
    }
}
