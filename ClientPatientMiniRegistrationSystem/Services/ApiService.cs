using System.Net.Http;
using System.Net.Http.Headers;

namespace PatientManagement.WPF.Services;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();

        _client.BaseAddress =
            new Uri("https://localhost:7033/");
    }

    public HttpClient Client => _client;

    public void SetToken(string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}