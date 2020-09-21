using System;
using Xunit;

namespace StringCalculator
{
    public class StringCalculatorTests
    {
        StringCalculator calculator;

        public StringCalculatorTests()
        {
            calculator = new StringCalculator();
        }

        
        [Fact]
        public void GivenEmptyString_ShouldReturn0()
        {
            var actual = calculator.Add("");
            var expected = 0;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]

        public void GivenNumber_ShouldReturnNumber(string input, int value)
        {
            var actual = calculator.Add(input);

            Assert.Equal(value, actual);

        }

        [Fact]
        public void GivenTwoNumbers_ShouldReturnSum()
        {
            var actual = calculator.Add("3,5");
            var expected = 8;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GivenAnyNumbers_ShouldReturnSum()
        {
            var actual = calculator.Add("3,5,3,9");
            var expected = 20;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GivenThereIsNewLineBreaksBetweenNumbers_ShouldReturnSum()
        {
            var actual = calculator.Add("3\n5\n3,9");
            var expected = 20;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GivenDifferentDelimiters_ShouldReturnSum()
        {
            var actual = calculator.Add("//;\n1;2");
            var expected = 3;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNegativeNumbers_ShouldThrowNegativeException()
        {
            Action act = () => calculator.Add("-1,2,-3");
            //assert
            NegativeNumberException exception = Assert.Throws<NegativeNumberException>(act);
            //The thrown exception can be used for even more detailed assertions.
            Assert.Equal("Negatives not allowed: -1, -3", exception.Message);
        }

        [Fact]
        public void GivenNumberLargerThan1000_ShouldBeIgnored()
        {
            var actual = calculator.Add("1000,1001,2");
            var expected = 2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDelimitersWithAnyLength_ShouldReturnSum()
        {
            var actual = calculator.Add("//[***]\n1***2***3");
            var expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenMultipleDelimiters_ShouldReturnSum()
        {
            var actual = calculator.Add("//[*][%]\n1*2%3");
            var expected = 6;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenMultipleDelimitersLongerThanOneChar_ShouldReturnSum()
        {
            var actual = calculator.Add("//[***][#][%]\n1***2#3%4");
            var expected = 10;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenDelimiterWithNumberInBetween_ShouldReturnSum()
        {
            var actual = calculator.Add("//[*1*][%]\n1*1*2%3");
            var expected = 6;

            Assert.Equal(expected, actual);
        }
    }
}
