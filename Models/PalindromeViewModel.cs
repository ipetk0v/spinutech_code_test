namespace SpinutechCodeTest.Models
{
    public class PalindromeViewModel : ExerciseViewModel
    {
        public int? Number { get; set; }
        public int? StartRange { get; set; }
        public int? EndRange { get; set; }
        public bool? IsPalindrome { get; set; }
        public int? NextPalindrome { get; set; }
        public List<int>? PalindromesInRange { get; set; }
    }
} 