```markdown
# EShop

A simple online shopping platform built with ASP.NET Core MVC, demonstrating essential e-commerce functionalities.


## 📂 Repository Structure

EShop/
├── EShop.sln                     # Visual Studio solution file
├── Controllers/                  # MVC controllers for handling requests
├── Models/                       # Business models and data structures
├── Views/                        # Razor views for UI rendering
├── Data/                         # Database context and migrations
├── Migrations/                   # EF Core migrations
├── wwwroot/                      # Static files (CSS, JS, images)
├── appsettings.json              # Application configuration
├── Program.cs                    # Application entry point
└── Startup.cs                    # Configuration and middleware setup

````

---

## 🔧 Technologies & Architecture

- **ASP.NET Core MVC**: For building the web application using the Model-View-Controller pattern.
- **Entity Framework Core**: For data access and ORM functionality.
- **Razor Views**: For dynamic HTML generation.
- **SQL Server**: As the database provider.
- **Bootstrap**: For responsive UI design.

---

## 🚀 Quick Start

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Docker, or production)
- Visual Studio 2022+ or VS Code

### Setup & Run

1. Clone the repository:
   ```bash
   git clone https://github.com/omidpurdarbani/EShop.git
   cd EShop
   ```

2. Open the solution in Visual Studio or your preferred IDE.

3. Configure your database connection in `appsettings.json`.

4. Apply migrations to set up the database:

   ```bash
   dotnet ef database update
   ```

5. Run the application:

   ```bash
   dotnet run
   ```

6. Navigate to `https://localhost:5001` to access the application.

---

## ✅ Features

* User registration and authentication
* Product catalog with categories
* Shopping cart functionality
* Order management
* Responsive design with Bootstrap

---

## 🧪 Running Tests

To run tests:

```bash
cd EShop.Tests
dotnet test
```

---

## 🛠️ Development Guidelines

* Follow the MVC pattern: Controllers handle user input, Models represent data, and Views display UI.
* Keep business logic in Models and Services.
* Use Entity Framework Core for data access.
* Ensure all new features are covered by tests.

---

## 🏗️ Contributing

* Fork the repository.
* Create a feature or bugfix branch.
* Implement your changes.
* Write tests for new features.
* Submit a pull request with a clear description of your changes.

---

## 📞 Contact

Omid Purdarbani – [omidcv.ir](https://omidcv.ir)

Feel free to open issues or submit pull requests!

---

## 📑 License

Released under the **MIT License**.
