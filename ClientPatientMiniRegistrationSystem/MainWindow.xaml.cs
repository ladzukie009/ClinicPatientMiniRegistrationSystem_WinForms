using PatientManagement.WPF.Helpers;
using PatientManagement.WPF.Models;
using PatientManagement.WPF.Services;
using PatientManagement.WPF.Views;
using System.Windows;
using System.Linq;

namespace PatientManagement.WPF;

public partial class MainWindow : Window
{
    private readonly PatientService _patientService;
    private List<Patient> _patients = new();

    public MainWindow()
    {
        InitializeComponent();

        var api = new ApiService();

        api.SetToken(TokenStorage.Token);

        _patientService = new PatientService(api);

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadPatients();
    }

    private async Task LoadPatients()
    {
        _patients = await _patientService.GetPatients();

        txtSearch.Clear();

        PatientsGrid.ItemsSource = _patients;
    }

    private Patient? SelectedPatient =>
        PatientsGrid.SelectedItem as Patient;

    private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
    {
        await LoadPatients();
    }

    private void BtnLogout_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show(
            "Are you sure you want to logout?",
            "Logout",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        TokenStorage.Token = "";

        LoginWindow login = new LoginWindow();

        login.Show();

        Close();
    }

    private async void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        var window = new PatientFormWindow();

        window.Owner = this;

        window.ShowDialog();

        if (window.SavedSuccessfully)
        {
            await LoadPatients();
        }
    }

    private async void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPatient == null)
        {
            MessageBox.Show("Please select a patient.");

            return;
        }

        PatientFormWindow window =
            new PatientFormWindow(SelectedPatient);

        window.Owner = this;

        window.ShowDialog();

        if (window.SavedSuccessfully)
        {
            await LoadPatients();
        }
    }

    private async void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPatient == null)
        {
            MessageBox.Show(
                "Please select a patient.",
                "Delete Patient",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        var result = MessageBox.Show(
            $"Delete patient\n\n{SelectedPatient.Name}?\n\nThis action cannot be undone.",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
            return;

        bool success =
            await _patientService.DeletePatient(SelectedPatient.Id);

        if (success)
        {
            MessageBox.Show(
                "Patient deleted successfully.",
                "Delete",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            await LoadPatients();
        }
        else
        {
            MessageBox.Show(
                "Unable to delete patient.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void BtnDetails_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPatient == null)
        {
            MessageBox.Show(
                "Please select a patient.",
                "Patient Details",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            return;
        }

        PatientDetailsWindow window =
            new PatientDetailsWindow(SelectedPatient);

        window.Owner = this;

        window.ShowDialog();
    }

    private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        string keyword = txtSearch.Text.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            PatientsGrid.ItemsSource = _patients;
            return;
        }

        var filtered = _patients.Where(p =>

            p.Name.ToLower().Contains(keyword)

            || p.Gender.ToLower().Contains(keyword)

            || p.ContactNumber.ToLower().Contains(keyword)

            || p.Address.ToLower().Contains(keyword)

            || p.Id.ToString().Contains(keyword)

        ).ToList();

        PatientsGrid.ItemsSource = filtered;
    }
}