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
                var numbersWithoutNewLine = numbers.Replace("//", "\n");
                var resultSplit = numbersWithoutNewLine.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                char delimiter;
                var isParse = char.TryParse(resultSplit[0], out delimiter);
                if (!isParse) throw new ArgumentException("Numbers format with delimiter is not correct");

                var numbersJoin = string.Join(delimiter.ToString(), resultSplit[1]);
                return SumStringWithCommas(numbersJoin, delimiter);
            }

            if (numbers.Contains("\n") || numbers.Contains(","))
            {
                var numbersWithoutNewLine = numbers.Replace("\n", ",");
                return SumStringWithCommas(numbersWithoutNewLine, ',');
            }

            return string.IsNullOrEmpty(numbers) ? 0 : Convert.ToInt32(numbers);
        }

        private static int SumStringWithCommas(string numbers, char delimiter)
        {
            var numbersSplit = numbers.Split(delimiter);
            return numbersSplit.Select(x => Convert.ToInt32(x)).Sum();
        }

        private static void CheckErrors(string numbers)
        {
            if (numbers.Contains("\n,"))
            {
                throw new ArgumentException("\n, is not authorized");
            }

            var regex = new Regex(@"-[0-9]");
            var matches = regex.Matches(numbers);
            if (matches.Count <= 0) return;

            var negativeNumbers = matches.Cast<Match>().Select(x => x.Value);
            var result = string.Join(",", negativeNumbers);
            throw new ArgumentException($"Negatives are not allowed ({result})");
        }
    }
}
