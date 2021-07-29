using System;
using System.Collections.Generic;
using System.Linq;


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

            var delims = GetDelims(numbers);

            numbers = DeleteDelimsAtBeginning(numbers);

            var listOfNumbers = ParseNumsFromString(numbers, delims);

            if (listOfNumbers.Any(x => x < 0))
            {
                throw new NegativesException("negatives not allowed: " + 
                    string.Join(" ", listOfNumbers.Where(x => x < 0)));
            }

            return listOfNumbers.Where(x => x < 1000).Sum();
        }

        private IEnumerable<string> GetDelims(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return GetCustomDelims(numbers);
            }

            return new List<string> { "," , @"\n"};
        }

        private string DeleteDelimsAtBeginning(string numbers)
        {
            return string.Join("", numbers
                .SkipWhile(x => !char.IsDigit(x) && x != '-'));
        }

        private IEnumerable<int> ParseNumsFromString(string numbers, IEnumerable<string> delims)
        {
            return numbers
                .Split(delims.ToArray(), StringSplitOptions.None)
                .Select(x => int.Parse(x));        
        }
       
        private IEnumerable<string> GetCustomDelims(string numbers)
        {
            List<string> delims = new List<string> { @"\n" };

            const int countOfSlashes = 2;

            numbers = numbers.Remove(0, countOfSlashes);

            while (!numbers.StartsWith(@"\n"))
            {
                int startIndexOfEdge = numbers.Contains("][") ? numbers.IndexOf("][") : numbers.IndexOf(@"]\n");

                // take the substring between the first sign [ and the first border ][ or ]\n
                string delim = numbers.Substring(1, startIndexOfEdge-1);

                delims.Add(delim);

                // remove our delimiter and the two more parentheses in which it is located
                numbers = numbers.Remove(0, delim.Length + 2);
               
            }

            return delims;
        }


    }

}
