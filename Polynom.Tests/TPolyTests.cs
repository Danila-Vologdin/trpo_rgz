using System;
using System.Runtime.CompilerServices;
using trpo_rgz;
using Xunit;
using Xunit.Abstractions;

namespace Polynom.Tests
{
    public class TPolyTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public TPolyTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(6, 5, 6, 6, 4)]
        [InlineData(9, 5, 9, 3, 0)]
        [InlineData(1, 0, 0, 0, 1)]
        public void CanGetMaxPower(int expected, int coef1, int power1,
            int coef2, int power2)
        {
            var member1 = new TMember(coef1, power1);
            var dict = new TPoly(coef2, power2);
            dict.Add(power1, member1);
            Assert.Equal(expected, dict.GetMaxPower());
        }


        [Theory]
        [InlineData(5, 5, 1, 6, 4)]
        [InlineData(-1, -1, 9, 3, 0)]
        [InlineData(0, 0, 0, 0, 1)]
        public void CanGetCoefAtPower(int expected, int coef1, int power1,
            int coef2, int power2)
        {
            var member1 = new TMember(coef1, power1);
            var dict = new TPoly(coef2, power2);
            dict.Add(power1, member1);
            Assert.Equal(expected, dict.GetCoefAtPower(power1));
        }

        [Fact]
        public void CanCompareEqualPolynoms()
        {
            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //3x^2+2x^4+1x^0
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(3, 2));
            poly2.Add(4, new TMember(2, 4));
            poly2.Add(0, new TMember(1, 0));
            Assert.True(poly1.IsEqual(poly2));
        }

        [Fact]
        public void CanCompareNotEqualPolynoms()
        {
            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //3x^2+0x^4+0x^0
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(3, 2));
            poly2.Add(4, new TMember(0, 4));
            poly2.Add(0, new TMember(0, 0));
            Assert.False(poly1.IsEqual(poly2));
        }

        [Fact]
        public void CanSumPolynoms()
        {
            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //-7x^2+0x^4+0x^0
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(-7, 2));
            poly2.Add(4, new TMember(0, 4));
            poly2.Add(0, new TMember(0, 0));

            var sum = poly1 + poly2;
            //должно быть равно -4x^2+2x^4+1x^0
            var expected = new TPoly();
            expected.Add(2, new TMember(-4, 2));
            expected.Add(4, new TMember(2, 4));
            expected.Add(0, new TMember(1, 0));
            Assert.True(expected.IsEqual(sum));

        }

        [Fact]
        public void CanSubtractPolynoms()
        {
            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //-7x^2+0x^4+0x^0-1x^1-5x^3
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(-7, 2));
            poly2.Add(4, new TMember(0, 4));
            poly2.Add(0, new TMember(0, 0));
            poly2.Add(1, new TMember(-1, 1));
            poly2.Add(3, new TMember(-5, 3));

            var res = poly1 - poly2;
            //должно быть равно 10x^2+2x^4+1x^0+1x^1+5x^3
            var expected = new TPoly();
            expected.Add(2, new TMember(10, 2));
            expected.Add(4, new TMember(2, 4));
            expected.Add(0, new TMember(1, 0));
            expected.Add(1, new TMember(1, 1));
            expected.Add(3, new TMember(5, 3));

            Assert.True(expected.IsEqual(res));

        }

        [Fact]
        public void CanMultiplyPolynoms()
        {
            var poly1 = new TPoly();
            //3x^2+2x^4+1x^0
            poly1.Add(2, new TMember(3, 2));
            poly1.Add(4, new TMember(2, 4));
            poly1.Add(0, new TMember(1, 0));
            //-7x^2+0x^4+0x^0-1x^1-5x^3
            var poly2 = new TPoly();
            poly2.Add(2, new TMember(-7, 2));
            poly2.Add(4, new TMember(0, 4));
            poly2.Add(0, new TMember(0, 0));
            poly2.Add(1, new TMember(-1, 1));
            poly2.Add(3, new TMember(-5, 3));

            var res = poly1 * poly2;
            //должно быть равно:
            //-10x^7-14x^6-17x^5-26x^4-10x^3-x^2
            var expected = new TPoly();
            expected.Add(7, new TMember(-10, 7));
            expected.Add(6, new TMember(-14, 6));
            expected.Add(5, new TMember(-17, 5));
            expected.Add(4, new TMember(-26, 4));
            expected.Add(3, new TMember(-10, 3));
            expected.Add(2, new TMember(-1, 2));

            Assert.True(expected.IsEqual(res));
            }

        [Fact]
        public void CanMultiplyTest1ForReport()
        {
            var poly1 = new TPoly();
            //0*X^0
            poly1.Add(0, new TMember(0, 0));
            //0*X^0
            var poly2 = new TPoly();
            poly2.Add(0, new TMember(0, 0));

            var res = poly1 * poly2;
            //должно быть равно:
            //0*X^0
            var expected = new TPoly();
            expected.Add(0, new TMember(0, 0));
            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanMultiplyTest2ForReport()
        {
            var poly1 = new TPoly();
            //0*X^0
            poly1.Add(0, new TMember(0, 0));
            //1*X^0
            var poly2 = new TPoly();
            poly2.Add(0, new TMember(1, 0));

            var res = poly1 * poly2;
            //должно быть равно:
            //0*X^0
            var expected = new TPoly();
            expected.Add(0, new TMember(0, 0));
            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanMultiplyTest3ForReport()
        {
            var poly1 = new TPoly();
            //1*X^0
            poly1.Add(0, new TMember(1, 0));
            //1*X^0
            var poly2 = new TPoly();
            poly2.Add(0, new TMember(1, 0));

            var res = poly1 * poly2;
            //должно быть равно:
            //1*X^0
            var expected = new TPoly();
            expected.Add(0, new TMember(1, 0));
            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanMultiplyTest4ForReport()
        {
            var poly1 = new TPoly();
            //1*X^0
            poly1.Add(0, new TMember(1, 0));
            //2*X^1
            var poly2 = new TPoly();
            poly2.Add(1, new TMember(2, 1));

            var res = poly1 * poly2;
            //должно быть равно:
            //2*X^1
            var expected = new TPoly();
            expected.Add(1, new TMember(2, 1));
            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanMultiplyTest5ForReport()
        {
            var poly1 = new TPoly();
            //1*X^0
            poly1.Add(0, new TMember(1, 0));
            //2*X^1+3*X^2
            var poly2 = new TPoly();
            poly2.Add(1, new TMember(2, 1));
            poly2.Add(2, new TMember(3, 2));

            var res = poly1 * poly2;
            //должно быть равно:
            //2*X^1+3*X^2
            var expected = new TPoly();
            expected.Add(1, new TMember(2, 1));
            expected.Add(2, new TMember(3, 2));

            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanMultiplyTest6ForReport()
        {
            var poly1 = new TPoly();
            //1*X^0+1*X^1
            poly1.Add(0, new TMember(1, 0));
            poly1.Add(1, new TMember(1, 1));

            //1*X^0-1*X^1
            var poly2 = new TPoly();
            poly2.Add(0, new TMember(1, 0));
            poly2.Add(1, new TMember(-1, 1));

            var res = poly1 * poly2;
            //должно быть равно:
            //1*X^0-1*X^2
            var expected = new TPoly();
            expected.Add(1, new TMember(1, 0));
            expected.Add(2, new TMember(-1, 2));

            Assert.True(expected.IsEqual(res));
        }

        [Fact]
        public void CanDiff()
        {
            //-7x^2-2x^4+0x^0-1x^1-5x^3
            var poly = new TPoly();
            poly.Add(2, new TMember(-7, 2));
            poly.Add(4, new TMember(-2, 4));
            poly.Add(0, new TMember(0, 0));
            poly.Add(1, new TMember(-1, 1));
            poly.Add(3, new TMember(-5, 3));

            var res = poly.Diff();
            var expected = new TPoly();
            expected.Add(1, new TMember(-14, 1));
            expected.Add(3, new TMember(-8, 3));
            expected.Add(0, new TMember(-1, 0));
            expected.Add(2, new TMember(-15, 2));
            _testOutputHelper.WriteLine(res.ToString());
            _testOutputHelper.WriteLine(expected.ToString());


            Assert.True(expected.IsEqual(res));

        }

        [Fact]
        public void CanCalculate()
        {
            //-7x^2-2x^4+0x^0-1x^1-5x^3
            var poly = new TPoly();
            poly.Add(2, new TMember(-7, 2));
            poly.Add(4, new TMember(-2, 4));
            poly.Add(0, new TMember(0, 0));
            poly.Add(1, new TMember(-1, 1));
            poly.Add(3, new TMember(-5, 3));

            double x = 1;
            var expected = -15;
            Assert.Equal(expected, poly.Calculate(x));
        }

        [Fact]
        public void CanToString()
        {
            //-7x^2-2x^4+0x^0-1x^1-5x^3
            var poly = new TPoly();
            poly.Add(2, new TMember(-7, 2));
            poly.Add(4, new TMember(-2, 4));
            poly.Add(0, new TMember(0, 0));
            poly.Add(1, new TMember(-1, 1));
            poly.Add(3, new TMember(-5, 3));
            var expected = "-7x^2-2x^4+0-1x^1-5x^3";
            Assert.Equal(expected, poly.ToString());
        }
    }
}
