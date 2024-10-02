# Human Resources Management System (HRMS)

**Author:** Priyanka Nikam

## Project Overview

HRMS is a web-based application built using ASP.NET Core MVC. The system helps manage employee data, leave requests, and payroll processing efficiently with a user-friendly interface.

## Features

- Employee Management: Add, edit, view, and delete employee records.
- Leave Management: Submit and manage leave requests with approval workflows.
- Payroll Processing: Manage employee salaries and payroll calculations.
- Role-Based Access: Different access levels for administrators, HR managers, and employees.
- Responsive UI: Built with Bootstrap and jQuery for seamless user experience.

## Libraries/Packages to Download

You need to ensure that the following libraries are installed in the project:

1. **Microsoft.EntityFrameworkCore.SqlServer** - To interact with the SQL Server database.
2. **Microsoft.EntityFrameworkCore.Tools** - For database migrations and tools.
3. **Microsoft.AspNetCore.Identity** - For user authentication and authorization.
4. **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation** - For runtime view compilation.
5. **Microsoft.Extensions.Logging.Console** - For logging to the console.
6. **Newtonsoft.Json** - For JSON serialization/deserialization.
7. **Bootstrap 4 or 5** - For responsive front-end design.
8. **jQuery** - For handling client-side scripting and dynamic UI interactions.

Install the necessary NuGet packages using the following command:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Identity
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Newtonsoft.Json
```

For **Bootstrap** and **jQuery**, add the following CDN links in your layout view:

```html
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
```

## Project Setup

### Step 1: Clone the Repository

```bash
git clone https://github.com/YourUsername/HRMS.git
```

### Step 2: Navigate to Project Directory

```bash
cd HRMS
```

### Step 3: Install Dependencies

Ensure you have the .NET SDK installed. Restore the project dependencies:

```bash
dotnet restore
```

### Step 4: Set Up the Database

1. Update the `appsettings.json` file with your database connection string.
2. Run database migrations:

```bash
dotnet ef database update
```

### Step 5: Run the Application

```bash
dotnet run
```

Access the application by visiting `http://localhost:5000` in your web browser.

---
