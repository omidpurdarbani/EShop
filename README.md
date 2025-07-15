```markdown
# ASP.NET Core gRPC

A high-performance gRPC service built with ASP.NET Core, implementing remote procedure calls for scalable backend-to-backend communication.


## 📂 Repository Structure

Asp.net-core-gRPC/
├── Asp.net-core-gRPC.sln              # Visual Studio solution
├── Protos/                            # .proto definitions for gRPC services and messages
├── GrpcService/                       # ASP.NET Core gRPC server implementation
│   ├── Services/                      # gRPC service handlers
│   ├── Program.cs                     # Host configuration
│   └── appsettings.json               # Configuration file
├── GrpcClient/                        # Console or demo gRPC client
│   ├── Program.cs                     # Client startup and demo calls
│   └── appsettings.json               # Client config (endpoints, options)
├── GrpcService.Tests/                 # Unit/integration tests for services
└── .gitignore, .gitattributes         # Git config files

````

---

## 🔧 Technologies & Architecture

- **.NET 6+ with ASP.NET Core** hosting high-performance gRPC
- **Protocol Buffers (.proto)** for strongly typed service and message definitions
- **gRPC client** for efficient communication over HTTP/2
- **Dependency Injection** to register and secure services
- **Integration testing** for contract-safe APIs
- **TLS/SSL support** via development certificates

---

## 🚀 Quick Start

### Prerequisites

- [.NET 6+ SDK](https://dotnet.microsoft.com)
- Development certificate (optional, auto-created by .NET Core)
- Visual Studio 2022+, JetBrains Rider, or VS Code

---

### Setup & Run

1. Clone the repository:

   ```bash
   git clone https://github.com/omidpurdarbani/Asp.net-core-gRPC.git
   cd Asp.net-core-gRPC
   ```

2. Build full solution:

   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the gRPC server:

   ```bash
   cd GrpcService
   dotnet run
   ```

   Server listens on `https://localhost:5001` (gRPC endpoint).

4. Run the demo client in another terminal:

   ```bash
   cd GrpcClient
   dotnet run
   ```

   The console client will make sample RPC calls and print results.

---

## ✅ Features

* **Unary**, **Server-streaming**, **Client-streaming** & **Bidirectional gRPC** endpoints
* Well-defined message types in `.proto` files
* Strongly typed C# clients and servers auto-generated
* Middleware support for logging, metrics, and exception handling
* Integration tests ensure RPC stability and response correctness

---

## 🧪 Testing

Run unit and integration tests:

```bash
cd GrpcService.Tests
dotnet test
```

Ensure all gRPC service contracts function as expected.

---

## 🛠️ Development Guidelines

* Define/extend services via `Protos/*.proto`
* Auto-generate C# models via MSBuild on build
* Implement server logic in `GrpcService/Services/*`
* Demonstrate usage in `GrpcClient/Program.cs`
* Write tests for all new gRPC service methods

---

## 🏗️ Contributing

* Fork the repo
* Create feature/bugfix branch
* Add/update `.proto`, server & client logic, and tests
* Submit PR with clear description of changes

---

## 📞 Contact

Omid Purdarbani – [omidcv.ir](https://omidcv.ir)

Issues and pull requests are welcome — happy to collaborate!

---

## 📑 License

This project is available under the **MIT License**.
