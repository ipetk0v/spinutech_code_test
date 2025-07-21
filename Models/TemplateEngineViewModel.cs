namespace SpinutechCodeTest.Models
{
    public class TemplateEngineViewModel : ExerciseViewModel
    {
        public string? Template { get; set; }
        public string? Variables { get; set; }
        public string? ProcessedTemplate { get; set; }
        public List<string>? ExtractedVariables { get; set; }
    }
} 