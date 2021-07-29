using Xunit;
using System;

namespace Kata.Tests
{
    public class CalculatorTests
    {
        private Calculator calculator;
        public CalculatorTests()
        {
            // Arrange
            calculator = new Calculator();
        }

        [Fact]
        public void Add_EmptyString_Zero()
        {
            // Act
            int result = calculator.Add("");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Add_OneNum_OneNum()
        {
            // Act
            int result = calculator.Add("1");

            // Assert
            Assert.Equal(1, result);            
        }

        [Fact]
        public void Add_TwoNums_SumOfThem()
        {
            // Act
            int result = calculator.Add("1,2");

            // Assert
            Assert.Equal(3,result);            
        }

        [Fact]
        public void Add_MoreThanTwoNumbers_SumOfThem()
        {
            // Act
            int result = calculator.Add("1,1,1");

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Add_NewLines_Sum()
        {
            // Act
            int result = calculator.Add(@"1\n2,3");

            // Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void Add_CustomDelims_Sum()
        {
            // Act
            int result = calculator.Add(@"//[!]\n1!2");

            // Assert
            Assert.Equal(3, result);           
        }

        [Fact]
        public void Add_Negatives_ThrowException()
        {
            // Act
            var exception = Assert.Throws(new NegativesException().GetType(), () => calculator.Add("1,-2,1"));

            // Assert
            Assert.Equal("negatives not allowed: -2", exception.Message);
        }

        [Fact]
        public void Add_OneNumBiggerThan1000_Ignore() 
        {
            // Act
            int result = calculator.Add("1000,2");

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Add_DelimWithAnyLength_Sum()
        {
            // Act
            int result = calculator.Add(@"//[***]\n1***2***3");

            // Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_MultipleDelims_Sum()
        {
            // Act
            int result = calculator.Add(@"//[*][%]\n1*2%3");

            // Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void Add_MultipleDelimsWithAnyLength_Sum()
        {
            // Act
            int result = calculator.Add(@"//[m]][%][!!!!!]\n1m]2%3\n4!!!!!5");

            // Assert
            Assert.Equal(15, result);
        }
    }
}
