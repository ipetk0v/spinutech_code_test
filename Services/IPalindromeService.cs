namespace SpinutechCodeTest.Services
{
    public interface IPalindromeService
    {
        bool IsPalindrome(int number);
        List<int> GetPalindromesInRange(int start, int end);
        int GetNextPalindrome(int number);
    }
} 