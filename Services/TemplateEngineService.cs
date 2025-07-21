using SpinutechCodeTest.Constants;

namespace SpinutechCodeTest.Services
{
    public class TemplateEngineService : ITemplateEngineService
    {
        public string ProcessTemplate(string template, Dictionary<string, string> variables)
        {
            if (string.IsNullOrEmpty(template)) return template;
            
            var result = new System.Text.StringBuilder();
            int i = 0;
            
            while (i < template.Length)
            {
                if (template[i] == '$' && i + 1 < template.Length && template[i + 1] == '{')
                {
                    // Check for escaped variable
                    if (i + 2 < template.Length && template[i + 2] == '$')
                    {
                        // This is an escaped variable, skip the first $ and continue
                        result.Append(AppConstants.TemplateEngine.DollarSign);
                        i += 2;
                        continue;
                    }
                    
                    // Find the closing brace
                    int startIndex = i + 2;
                    int endIndex = FindClosingBrace(template, startIndex);
                    
                    if (endIndex == -1)
                    {
                        throw new TemplateException(string.Format(AppConstants.TemplateEngine.UnclosedVariablePlaceholder, i));
                    }
                    
                    // Extract variable name
                    string variableName = template.Substring(startIndex, endIndex - startIndex).Trim();
                    
                    if (string.IsNullOrEmpty(variableName))
                    {
                        throw new TemplateException(string.Format(AppConstants.TemplateEngine.EmptyVariableName, i));
                    }
                    
                    // Check if variable exists
                    if (!variables.ContainsKey(variableName))
                    {
                        throw new TemplateException(string.Format(AppConstants.TemplateEngine.VariableNotDefined, variableName));
                    }
                    
                    // Replace with variable value
                    result.Append(variables[variableName]);
                    i = endIndex + 1;
                }
                else
                {
                    result.Append(template[i]);
                    i++;
                }
            }
            
            return result.ToString();
        }
        
        private int FindClosingBrace(string template, int startIndex)
        {
            int braceCount = 0;
            
            for (int i = startIndex; i < template.Length; i++)
            {
                if (template[i] == '{')
                {
                    braceCount++;
                }
                else if (template[i] == '}')
                {
                    if (braceCount == 0)
                    {
                        return i;
                    }
                    braceCount--;
                }
            }
            
            return -1; // Not found
        }
        
        public List<string> ExtractVariables(string template)
        {
            var variables = new List<string>();
            int i = 0;
            
            while (i < template.Length)
            {
                if (template[i] == '$' && i + 1 < template.Length && template[i + 1] == '{')
                {
                    // Skip escaped variables
                    if (i + 2 < template.Length && template[i + 2] == '$')
                    {
                        i += 3;
                        continue;
                    }
                    
                    int startIndex = i + 2;
                    int endIndex = FindClosingBrace(template, startIndex);
                    
                    if (endIndex != -1)
                    {
                        string variableName = template.Substring(startIndex, endIndex - startIndex).Trim();
                        if (!string.IsNullOrEmpty(variableName) && !variables.Contains(variableName))
                        {
                            variables.Add(variableName);
                        }
                        i = endIndex + 1;
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }
            
            return variables;
        }
    }

    public class TemplateException : Exception
    {
        public TemplateException(string message) : base(message) { }
    }
} 