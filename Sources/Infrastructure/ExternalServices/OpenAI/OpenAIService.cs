using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Utils.Singletons;

namespace ExternalServices.OpenAI;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAIService(string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _apiKey = EnvironementSingleton.GetEnvironmentVariable("OPENAI_API)KEY");
    }

    public async Task<string> GetCompletionAsync(string prompt, string model, int maxTokens)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "engines/" + model + "/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var requestBody = new
        {
            prompt = prompt,
            max_tokens = maxTokens
        };

        string requestBodyJson = JsonSerializer.Serialize(requestBody);
        request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string responseJson = await response.Content.ReadAsStringAsync();
        dynamic responseData = JsonSerializer.Deserialize<object>(responseJson);
        return responseData.choices[0].text;
    }
}