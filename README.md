# Clinic Patient Mini Registration System - WPF Desktop Application

A desktop application built with **WPF (.NET 8)** that consumes the **Clinic API**. The application allows authenticated users to manage patient records through a user-friendly interface.

## Features

* User Login with JWT Authentication
* View Patient List
* Add New Patient
* Edit Patient Information
* Delete Patient Records
* View Patient Details
* Search Patients
* Logout with Confirmation
* Automatic Data Refresh

---

# Prerequisites

Before running the application, ensure you have the following installed:

* Visual Studio 2022 (Community or later)
* .NET 8 SDK
* The **Clinic API** project

> **Important:** The Web API must be running before starting the WPF application.

---

# Clone the Repository

```bash
git clone https://github.com/yourusername/ClinicPatientMiniRegistrationSystem_WinForms.git
```

Or download the ZIP file and extract it.

---

# Open the Solution

Open the solution in **Visual Studio 2022**.

```
HospitalWPF.sln
```

---

# Restore NuGet Packages

If Visual Studio does not restore packages automatically, run:

```bash
dotnet restore
```

---

# Configure the API URL

Locate the API base URL in your project (for example, `ApiService.cs`, `App.xaml.cs`, or wherever your `HttpClient` is configured).

Example:

```csharp
private const string BaseUrl = "https://localhost:7078/api/";
```

or

```csharp
_httpClient.BaseAddress = new Uri("https://localhost:7078/");
```

Replace the URL if your API is running on a different port.

---

# Run the ASP.NET Core Web API

Start the Hospital API project first.

Verify Swagger is accessible:

```
https://localhost:7078/swagger
```

If Swagger opens successfully, the API is running correctly.

---

# Run the WPF Application

Press:

```
F5
```

or

```
Ctrl + F5
```

The login window should appear.

---

# Login

Use an existing account created in the Web API.

Example:

```
Username: admin
Password: admin123
```

If no users exist, register one using the API or Swagger before logging in.

---

# Project Structure

```
HospitalWPF
│
├── Models
│   ├── Patient.cs
│   └── LoginResponse.cs
|   └── LoginRequest.cs
│
├── Services
│   ├── ApiService.cs
│   └── AuthService.cs
│   └── PatientService.cs
│
├── Views
│   ├── LoginWindow.xaml
│   ├── PatientFormWindow.xaml
│   └── PatientDetailsWindow.xaml
│
├── ViewModels
│
├── Helpers
│
├── App.xaml
│
└── MainWindow.xaml
```

---

# Technologies Used

* WPF (.NET 8)
* C#
* HttpClient
* JWT Authentication
* ASP.NET Core Web API
* SQLite (through the API)

---

# Troubleshooting

### Unable to Login

* Ensure the Web API is running.
* Verify the API URL configured in the WPF application.
* Confirm the username and password are correct.

---

### Unable to Load Patients

* Check that the API is running.
* Verify your JWT token has not expired.
* Ensure your API URL is correct.

---

### SSL Certificate Error

If using HTTPS during development, trust the development certificate:

```bash
dotnet dev-certs https --trust
```

Restart both the API and the WPF application after trusting the certificate.

---

### Connection Refused

This usually means the API is not running or is using a different port.

Verify the API URL configured in the WPF application matches the URL displayed by the API when it starts.

---

# Development Notes

* This application communicates with the Hospital ASP.NET Core Web API.
* Patient data is stored in SQLite through the API.
* JWT tokens are used for authenticated requests.
* All CRUD operations are performed through REST API endpoints.
* Search functionality filters patient records.
* Delete and logout actions require user confirmation.

---

# Running Both Projects

1. Start the **Clinic API**.
2. Wait until it launches successfully.
3. Start the **Patient Management WPF Desktop Application**.
4. Log in using a registered account.
5. Begin managing patient records.

---
