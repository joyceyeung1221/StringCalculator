using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace StringCalculator
{
    public class StringCalculator
    {
        public StringCalculator()
        {
        }

        private List<string> _delimiters = new List<string> { ",", "\n" };
        private List<int> negativeNumbers = new List<int>();
        const int maxInputValue = 1000;
        const int minInputValue = 0;

        public int Add(string input)
        {
            var inputs = SplitInputByDelimitersIfAny(input);

            var arrayOfNumbers = RemoveInvalidInput(inputs);

            if (negativeNumbers.Count > 0)
            {
                throw new NegativeNumberException($"Negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return arrayOfNumbers.Sum();

        }

        private int[] RemoveInvalidInput(string[] inputs)
        {
            var filteredInputs = RemoveNonNumericValue(inputs);
            var numberListToBeSummed = RemoveNumberOutOfDefinedRange(filteredInputs);
            return numberListToBeSummed;
        }

        private int[] RemoveNumberOutOfDefinedRange(string[] filteredInputs)
        {
            List<int> filteredList = new List<int>();
            foreach (string value in filteredInputs)
            {
                var inputNumber = Int32.Parse(value);
                switch (inputNumber)
                {
                    case int n when n >= maxInputValue:
                        break;
                    case int n when n < minInputValue:
                        negativeNumbers.Add(n);
                        break;
                    default:
                        filteredList.Add(inputNumber);
                        break;
                }
            }
            return filteredList.ToArray();
        }

        private string[] RemoveNonNumericValue(string[] inputs)
        {
            return inputs.Where(input => Int32.TryParse(input, out int number)).ToArray();

        }

        private string[] SplitInputByDelimitersIfAny(string input)
        {
            if (HasCustomDelimiter(input, out Match matchResult))
            {
                AddDelimiter(matchResult);
            }

            string[] inputs = input.Split(_delimiters.ToArray(), StringSplitOptions.None);

            return inputs;
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