using PatientManagement.WPF.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace PatientManagement.WPF.Services;

public class PatientService
{
    private readonly ApiService _api;

    public PatientService(ApiService api)
    {
        _api = api;
    }

    public async Task<List<Patient>> GetPatients()
    {
        var response =
            await _api.Client.GetAsync("api/patient");

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<Patient>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<bool> CreatePatient(Patient patient)
    {
        var json = JsonSerializer.Serialize(patient);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _api.Client.PostAsync(
            "api/patient",
            content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            MessageBox.Show(error);
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePatient(Patient patient)
    {
        var json = JsonSerializer.Serialize(patient);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _api.Client.PutAsync(
            $"api/patient/{patient.Id}",
            content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePatient(int id)
    {
        var response = await _api.Client.DeleteAsync($"api/patient/{id}");

        return response.IsSuccessStatusCode;
    }
}