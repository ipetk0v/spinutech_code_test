namespace SpinutechCodeTest.Services
{
    public class NumberToWordsConverter : INumberToWordsConverter
    {
        private static readonly string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static readonly string[] Thousands = { "", "Thousand", "Million", "Billion" };

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
                    words = groupWords + (thousandsIndex > 0 ? " " + Thousands[thousandsIndex] + " " : "") + words;
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
                words += Ones[group / 100] + " Hundred ";
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

        public static string ConvertToWords(decimal amount)
        {
            if (amount == 0) return "Zero dollars";

            long dollars = (long)Math.Floor(amount);
            int cents = (int)Math.Round((amount - dollars) * 100);

            string result = ConvertDollarsToWords(dollars);

            if (cents > 0)
            {
                result += $" and {cents:D2}/100";
            }

            result += " dollars";
            return result;
        }
    }
}
