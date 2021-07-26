using System;


namespace Kata
{
    public class ConsoleImitator
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine().Replace("\\n", "\n");
        }

        public virtual void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
