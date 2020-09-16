using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace StringCalculator
{
    public class StringCalculator
    {
        public StringCalculator()
        {
        }

        private List<string> _delimiters = new List<string> { ",", "\n" };

        public int Calculate(string input)
        {

            if (HasCustomDelimiter(input, out Match matchResult))
            {
                AddDelimiter(matchResult);
            }

            string[] inputs = input.Split(_delimiters.ToArray(), StringSplitOptions.None);

            int sumResult = 0;
            List<int> negativeNumbers = new List<int>();
            foreach (var splitstring in inputs)
            {
                if (Int32.TryParse(splitstring, out int number))
                {
                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                    else if (number < 1000)
                    {
                        sumResult += number;
                    }
                }

            }
            if (negativeNumbers.Count > 0)
            {
                throw new NegativeNumberException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return sumResult;


        }

        private bool HasCustomDelimiter(string input, out Match m)
        {
            string pattern = @"\/\/(.+?)\n";
            Regex regex = new Regex(pattern);
            m = regex.Match(input);
            return m.Success;

        }

        private void AddDelimiter(Match matchResult)
        {
            string delimiterField = matchResult.Groups[1].Value;
            if (HasMultipleDelimilters(delimiterField, out MatchCollection m))
            {
                foreach (Match match in m)
                {
                    _delimiters.Add(match.Groups[1].Value);
                }
            }
            else
            {
                _delimiters.Add(delimiterField);
            }
        }

        private bool HasMultipleDelimilters(string delimiterField, out MatchCollection m)
        {
            string pattern = @"\[(.+?)\]";
            Regex regex = new Regex(pattern);
            m = regex.Matches(delimiterField);
            return m.Count > 0;

        }
    }
}