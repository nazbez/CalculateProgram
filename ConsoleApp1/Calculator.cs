using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Kata
{
    public class Calculator
    {
        public virtual int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            var delims = GetDelims(ref numbers);

            var listOfNumbers = ParseNumsFromString(numbers, delims);

            if (listOfNumbers.Any(x => x < 0))
            {
                throw new Exception("negatives not allowed: " + string.Join(" ", listOfNumbers.Where(x => x < 0)));
            }

            return listOfNumbers.Where(x => x < 1000).Sum();
        }

        private IEnumerable<string> GetDelims(ref string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return GetUndefaultDelims(ref numbers);
            }

            return new List<string> { "," };
        }

        private IEnumerable<int> ParseNumsFromString(string numbers, IEnumerable<string> delims)
        {
            StringBuilder numbersSB = new StringBuilder(numbers);

            foreach (var delim in delims)
            {
                numbersSB.Replace(delim, " ");
            }

            return numbersSB.Replace("\n", " ").ToString()
                .Split(" ").Select(x => int.Parse(x)).ToList();            
        }
       
        private IEnumerable<string> GetUndefaultDelims(ref string numbers)
        {
            List<string> delims = new List<string> { };

            const int countOfSlashes = 2;

            numbers = numbers.Remove(0, countOfSlashes);

            while (!numbers.StartsWith("\n"))
            {
                int startIndexOfEdge = numbers.Contains("][") ? numbers.IndexOf("][") : numbers.IndexOf("]\n");

                string delim = numbers.Substring(1, startIndexOfEdge-1);

                delims.Add(delim);

                numbers = numbers.Remove(0, delim.Length + 2);
               
            }

            numbers = numbers.Remove(0,1);

            return delims;
        }
    }

}
