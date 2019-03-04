using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator.Kata.Services;

namespace StringCalculator.Kata.Tests
{
    [TestClass]
    public class StringCalculatorServiceTest
    {
        private readonly StringCalculatorService _stringCalculatorService;

        public StringCalculatorServiceTest()
        {
            _stringCalculatorService = new StringCalculatorService();
        }

        [TestMethod]
        public void should_return_0_when_string_is_empty()
        {
            var numbers = string.Empty;
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void should_return_number_when_string_contains_only_one_number()
        {
            var numbers = "1";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void should_return_result_addition_when_string_contains_two_numbers()
        {
            var numbers = "1,3";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void should_return_result_addition_when_string_contains_multiple_numbers()
        {
            var numbers = "1,3,5,8,4";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void should_return_result_addition_when_string_contains_new_lines()
        {
            var numbers = "1\n2,3";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void should_return_exception_when_string_contains_new_lines_and_comma_pasted()
        {
            var numbers = "1\n,2,3";
            _stringCalculatorService.Add(numbers);
        }

        [TestMethod]
        public void should_return_result_addition_when_user_change_delimiter()
        {
            var numbers = "//;\n1;2";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void should_return_result_addition_when_user_change_delimiter_with_multiple_caracters()
        {
            var numbers = "//[***]\n1***2***3";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Negatives are not allowed (-3)")]
        public void should_return_exception_when_string_contains_negatives_numbers()
        {
            var numbers = "1,2,-3";
            _stringCalculatorService.Add(numbers);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Negatives are not allowed (-3,-4)")]
        public void should_return_exception_when_string_contains_multiple_negatives_numbers()
        {
            var numbers = "1,2,-3,-4";
            _stringCalculatorService.Add(numbers);
        }

        [TestMethod]
        public void should_ignore_number_more_than_1000_and_add_others()
        {
            var numbers = "1,2,1002";
            var result = _stringCalculatorService.Add(numbers);
            Assert.AreEqual(3, result);
        }
    }
}
