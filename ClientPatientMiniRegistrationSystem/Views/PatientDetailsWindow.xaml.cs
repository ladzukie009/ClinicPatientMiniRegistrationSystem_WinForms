using System.Windows;
using PatientManagement.WPF.Models;

namespace PatientManagement.WPF.Views;

public partial class PatientDetailsWindow : Window
{
    public PatientDetailsWindow(Patient patient)
    {
        InitializeComponent();

        txtName.Text = patient.Name;

        txtGender.Text = patient.Gender;

        txtBirthDate.Text =
            patient.BirthDate.ToShortDateString();

        txtContact.Text =
            patient.ContactNumber;

        txtAddress.Text =
            patient.Address;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}