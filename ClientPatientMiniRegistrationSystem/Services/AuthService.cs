using PatientManagement.WPF.Helpers;
using PatientManagement.WPF.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PatientManagement.WPF.Services;

public class AuthService
{
    private readonly ApiService _api;

    public AuthService(ApiService api)
    {
        _api = api;
    }

    public async Task<bool> Login(LoginRequest request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response =
            await _api.Client.PostAsync(
                "api/auth/login",
                content);

        if (!response.IsSuccessStatusCode)
            return false;

        var body =
            await response.Content.ReadAsStringAsync();

        var login =
            JsonSerializer.Deserialize<LoginResponse>(
                body,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        TokenStorage.Token = login!.Token;

        _api.SetToken(login.Token);

        return true;
    }
}