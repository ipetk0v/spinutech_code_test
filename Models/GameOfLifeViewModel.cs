namespace SpinutechCodeTest.Models
{
    public class GameOfLifeViewModel : ExerciseViewModel
    {
        public string? BoardInput { get; set; }
        public int? Generations { get; set; }
        public string? FinalBoard { get; set; }
        public int? BoardRows { get; set; }
        public int? BoardCols { get; set; }
    }
} 