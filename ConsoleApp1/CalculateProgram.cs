using System;

namespace Kata
{
    public class CalculateProgram
    {
        private readonly Calculator _calculator;
        private readonly ConsoleImitator _console;

        public CalculateProgram(Calculator calculator, ConsoleImitator console)
        {
            _calculator = calculator;
            _console = console;
        }

        public void Interract()
        {
            int state = InputOutput("Enter comma separated numbers (enter to exit):");

            while (state != -1)
            {
                state = InputOutput("You can enter other numbers (enter to exit)?");
            }
        }

        private int InputOutput(string message)
        {
            _console.WriteLine(message);
            string numbers = _console.ReadLine();

            if (string.IsNullOrEmpty(numbers))
            {
                return -1;
            }

            try
            {
                int result = _calculator.Add(numbers);
                _console.WriteLine($"Result is: {result}");
            }
            catch (Exception err)
            {
                _console.WriteLine($"Error: {err.Message}");
            }

            return 0;
        }
    }
}
