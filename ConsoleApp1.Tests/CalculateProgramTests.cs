using System;
using Moq;
using Xunit;

namespace Kata.Tests
{
    public class CalculateProgramTests
    {
        private Mock<ConsoleImitator> mockConsole;
        private Mock<Calculator> mockCalculator;
        private CalculateProgram calculateProgram;

        public CalculateProgramTests()
        {
            mockConsole = new Mock<ConsoleImitator>();
            mockCalculator = new Mock<Calculator>();
            calculateProgram = new CalculateProgram(mockCalculator.Object, mockConsole.Object);
        }

        [Fact]
        public void Interract_PressEnterAtTheStart_HelloMsg()
        { 
            // Arrange
            mockConsole.Setup(x => x.ReadLine()).Returns("");

            // Act
            calculateProgram.Interract();
        
            // Assert
            mockConsole.Verify(x => x.WriteLine("Enter comma separated numbers (enter to exit):"), Times.Once);
        }

        [Fact]
        public void Interract_EnterTheInputOnce_ResultAndOneOfferMsg()
        {
            // Arrange
            mockConsole.SetupSequence(x => x.ReadLine()).Returns("1,2").Returns("");
            mockCalculator.Setup(x => x.Add("1,2")).Returns(3);

            // Act
            calculateProgram.Interract();

            // Assert
            mockConsole.Verify(x => x.WriteLine("Result is: 3"), Times.Once);
            mockConsole.Verify(x => x.WriteLine("You can enter other numbers (enter to exit)?"), Times.Once);

        }

        [Fact]
        public void Interract_EnterTheInputMoreThanOnce_ResultsAndOfferMoreThanOnce()
        {
            // Arrange
            mockConsole.SetupSequence(x => x.ReadLine()).Returns("1,2").Returns("3,4").Returns("");
            mockCalculator.SetupSequence(x => x.Add(It.IsAny<string>())).Returns(3).Returns(7);

            // Act
            calculateProgram.Interract();

            // Assert
            mockConsole.Verify(x => x.WriteLine("Result is: 7"), Times.Once);
            mockConsole.Verify(x => x.WriteLine("You can enter other numbers (enter to exit)?"), Times.Exactly(2));
        }

        [Fact]
        public void Interract_EnterTheInvalidInput_ShowException()
        {
            // Arrange
            mockConsole.SetupSequence(x => x.ReadLine()).Returns("1,2").Returns("Test").Returns("");
            mockCalculator.SetupSequence(x => x.Add(It.IsAny<string>())).Returns(3).Throws(new Exception("Invalid input!"));

            // Act
            calculateProgram.Interract();

            // Assert
            mockConsole.Verify(x => x.WriteLine("Error: Invalid input!"), Times.Once);
        }

        [Fact]
        public void Interract_EnterNumberWithMinus_ShowException()
        {
            // Arrange
            mockConsole.SetupSequence(x => x.ReadLine()).Returns("1,2").Returns("1,-2,-3").Returns("");
            mockCalculator.SetupSequence(x => x.Add(It.IsAny<string>())).Returns(3).Throws(new Exception("negatives not allowed: -2 -3"));

            // Act
            calculateProgram.Interract();

            // Assert
            mockConsole.Verify(x => x.WriteLine("Error: negatives not allowed: -2 -3"), Times.Once);
        }

    }
}
