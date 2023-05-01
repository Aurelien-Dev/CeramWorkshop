namespace ExternalServices.OpenAI;

public interface IOpenAIService
{
    Task<string> GetCompletionAsync(string prompt, string model, int maxTokens);
}