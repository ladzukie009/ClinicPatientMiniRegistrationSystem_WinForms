using PatientManagement.WPF.Helpers;
using PatientManagement.WPF.Models;
using PatientManagement.WPF.Services;
using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.WPF.Views;

public partial class PatientFormWindow : Window
{
    private readonly PatientService _patientService;

    private readonly bool _isEdit;

    private Patient? _patient;

    public bool SavedSuccessfully { get; private set; }

    public PatientFormWindow(Patient? patient = null)
    {
        InitializeComponent();

        var api = new ApiService();

        api.SetToken(TokenStorage.Token);

        _patientService = new PatientService(api);

        if (patient != null)
        {
            _isEdit = true;

            _patient = patient;

            Title = "Edit Patient";

            txtName.Text = patient.Name;

            dpBirthDate.SelectedDate = patient.BirthDate;

            txtContact.Text = patient.ContactNumber;

            txtAddress.Text = patient.Address;

            foreach (ComboBoxItem item in cmbGender.Items)
            {
                if (item.Content!.ToString() == patient.Gender)
                {
                    cmbGender.SelectedItem = item;
                    break;
                }
            }
        }
        else
        {
            Title = "Add Patient";
        }
    }

    private async void btnSave_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Name is required.");
            return;
        }

        if (cmbGender.SelectedItem == null)
        {
            MessageBox.Show("Please select a gender.");
            return;
        }

        var patient = new Patient
        {
            Id = _patient?.Id ?? 0,

            Name = txtName.Text,

            BirthDate = dpBirthDate.SelectedDate ?? DateTime.Today,

            Gender = ((ComboBoxItem)cmbGender.SelectedItem)
                        .Content!
                        .ToString()!,

            ContactNumber = txtContact.Text,

            Address = txtAddress.Text
        };

        bool success;

        if (_isEdit)
            success = await _patientService.UpdatePatient(patient);
        else
            success = await _patientService.CreatePatient(patient);

        if (success)
        {
            SavedSuccessfully = true;

            MessageBox.Show(
                _isEdit
                ? "Patient updated successfully."
                : "Patient created successfully.");

            Close();
        }
        else
        {
            MessageBox.Show("Operation failed.");
        }
    }
}