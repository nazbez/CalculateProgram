using System;

namespace Kata
{
    public class Program
    {
        static void Main(string[] args)
        {
            CalculateProgram program = new CalculateProgram(new Calculator(), new ConsoleImitator());
            program.Interract();
        }
    }
}