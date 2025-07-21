using SpinutechCodeTest.Constants;

namespace SpinutechCodeTest.Services
{
    public class NumberToWordsService : INumberToWordsService
    {
        private static readonly string[] Ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static readonly string[] Teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] Tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] Thousands = { "", "thousand", "million", "billion" };

        public string ConvertToWords(decimal amount)
        {
            if (amount == 0) return AppConstants.NumberToWords.ZeroDollars;

            long dollars = (long)Math.Floor(amount);
            int cents = (int)Math.Round((amount - dollars) * 100);

            string result = ConvertDollarsToWords(dollars);
            
            if (cents > 0)
            {
                result += AppConstants.NumberToWords.And + $"{cents:D2}" + AppConstants.NumberToWords.Slash100;
            }
            
            result += AppConstants.NumberToWords.Dollars;
            return ToTitleCase(result);
        }

        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            
            // Make only the first letter of the entire sentence uppercase
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        private static string ConvertDollarsToWords(long dollars)
        {
            if (dollars == 0) return "";

            string words = "";
            int thousandsIndex = 0;

            while (dollars > 0)
            {
                int group = (int)(dollars % 1000);
                if (group != 0)
                {
                    string groupWords = ConvertGroupToWords(group);
                    words = groupWords + (thousandsIndex > 0 ? AppConstants.NumberToWords.Space + Thousands[thousandsIndex] + AppConstants.NumberToWords.Space : "") + words;
                }
                dollars /= 1000;
                thousandsIndex++;
            }

            return words.Trim();
        }

        private static string ConvertGroupToWords(int group)
        {
            if (group == 0) return "";

            string words = "";

            // Handle hundreds
            if (group >= 100)
            {
                words += Ones[group / 100] + AppConstants.NumberToWords.Hundred;
                group %= 100;
            }

            // Handle tens and ones
            if (group >= 10)
            {
                if (group < 20)
                {
                    words += Teens[group - 10];
                }
                else
                {
                    words += Tens[group / 10];
                    if (group % 10 > 0)
                    {
                        words += "-" + Ones[group % 10];
                    }
                }
            }
            else if (group > 0)
            {
                words += Ones[group];
            }

            return words;
        }
    }
} 