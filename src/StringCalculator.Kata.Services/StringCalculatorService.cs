using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator.Kata.Services
{
    public class StringCalculatorService
    {
        public int Add(string numbers)
        {
            CheckErrors(numbers);

            if (numbers.Contains("//") && numbers.Contains("\n"))
            {
                var newString = numbers.Replace("//", "\n");
                var result = newString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                char delimiter;
                var isParse = char.TryParse(result[0], out delimiter);
                if(isParse)
                {
                    var allnumbers = string.Join(delimiter.ToString(), result[1]);
                    return SumStringWithCommas(allnumbers, delimiter);
                }
            }
            else if (numbers.Contains("\n") || numbers.Contains(","))
            {
                var numbersWithoutNewLine = numbers.Replace("\n", ",");
                return SumStringWithCommas(numbersWithoutNewLine, ',');
            }

            return string.IsNullOrEmpty(numbers) ? 0 : Convert.ToInt32(numbers);
        }

        private static int SumStringWithCommas(string numbers, char delimiter)
        {
            var allnumbers = numbers.Split(delimiter);
            return allnumbers.Select(x => Convert.ToInt32(x)).Sum();
        }

        private static void CheckErrors(string numbers)
        {
            if (numbers.Contains("\n,"))
            {
                throw new ArgumentException("\n, is not authorized");
            }
            else
            {
                var regex = new Regex(@"-[0-9]");
                MatchCollection matches = regex.Matches(numbers);
                if (matches.Count > 0)
                {
                    var negativeNumbers = matches.Cast<Match>().Select(x => x.Value);
                    var result = string.Join(",", negativeNumbers);
                    throw new ArgumentException($"Negatives are not allowed (result)");
                }
            }
        }
    }
}
