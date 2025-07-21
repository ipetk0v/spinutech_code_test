namespace SpinutechCodeTest.Models
{
    // Base ViewModel for all exercises
    public abstract class ExerciseViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Result { get; set; }
        public string? ErrorMessage { get; set; }
    }
} 