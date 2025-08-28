using System.Text;
using System.Text.Json;

public class ApiService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public ApiService(string apiUrl)
    {
        _httpClient = new HttpClient();
        _apiUrl = apiUrl;

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "JurisprudenciaParser/1.0");
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task<HttpResponseMessage> EnviarDadosJurisprudenciaAsync(List<JurisprudenciaItem> dados)
    {
        try
        {
            var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao enviar dados para a API: {ex.Message}", ex);
        }
    }
}