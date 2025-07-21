namespace SpinutechCodeTest.Services
{
    public interface ITemplateEngineService
    {
        string ProcessTemplate(string template, Dictionary<string, string> variables);
        List<string> ExtractVariables(string template);
    }
} 