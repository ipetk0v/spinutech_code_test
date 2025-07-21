namespace SpinutechCodeTest.Services
{
    public class PalindromeService : IPalindromeService
    {
        public bool IsPalindrome(int number)
        {
            if (number < 0) return false;
            if (number < 10) return true; // Single digit is always palindrome
            if (number % 10 == 0) return false; // Numbers ending with 0 can't be palindromes
            
            int reversedHalf = 0;
            
            while (number > reversedHalf)
            {
                reversedHalf = reversedHalf * 10 + number % 10;
                number /= 10;
            }
            
            // For even digits: number == reversedHalf
            // For odd digits: number == reversedHalf / 10 (remove middle digit)
            return number == reversedHalf || number == reversedHalf / 10;
        }

        public List<int> GetPalindromesInRange(int start, int end)
        {
            var palindromes = new List<int>();
            
            for (int i = start; i <= end; i++)
            {
                if (IsPalindrome(i))
                {
                    palindromes.Add(i);
                }
            }
            
            return palindromes;
        }

        public int GetNextPalindrome(int number)
        {
            int next = number + 1;
            while (!IsPalindrome(next))
            {
                next++;
            }
            return next;
        }
    }
} 