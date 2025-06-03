# 🚀 OrgManager-API

OrgManager-API is a RESTful web API built with ASP.NET Core (.NET 8) for managing organizational departments and employees. It features secure authentication and authorization using ASP.NET Core Identity and JWT tokens, and supports role-based access control for sensitive operations.

---

## ✨ Features

- **User Registration & Authentication** 🔐
  - Register new users with unique usernames and emails.
  - Secure login with JWT token issuance.
  - Password policies enforced via Identity options.

- **Role-Based Authorization** 🛡️
  - Supports roles such as `Admin` and `User`.
  - Admins can create, update, and delete departments.
  - Users can view department and employee information.
  - Assign roles to users via a dedicated endpoint.

- **Department Management** 🏢
  - Create, update, delete, and retrieve departments.
  - Retrieve departments with their associated employees.

- **Employee Management** 👥
  - Create, update, delete, and retrieve employees.
  - Retrieve employees with their associated departments.

- **Swagger/OpenAPI Documentation** 📖
  - Interactive API documentation and testing via Swagger UI.
  - JWT Bearer authentication supported in Swagger.

---

## 🛠️ Technologies Used

- ASP.NET Core 8 (Web API)
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger/OpenAPI

---

## 🚦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or update the connection string for your DB)
- (Optional) Visual Studio 2022 or later

### Setup

1. **Clone the repository:**  

2. **Configure the database:**  
   - Update the `appsettings.json` or user secrets with your SQL Server connection string under `ConnectionStrings:cs`.

3. **Apply migrations**  
     
4. **Run the application**

5. **Access Swagger UI:**  
   - Navigate to `https://localhost:<port>/swagger` in your browser.

### 🌱 Seeding Roles

To ensure roles like `Admin` and `User` exist, uncomment and use the `SeedRolesAsync` method in `Program.cs` on first run.

---

## 📚 API Endpoints

### Authentication

- `POST /api/Account/Register` — Register a new user.
- `POST /api/Account/Login` — Authenticate and receive a JWT token.
- `POST /api/Account/AssignRole` — Assign a role to a user (Admin only).

### Departments

- `GET /api/Department` — List all departments (with employees).
- `GET /api/Department/{id}` — Get a department by ID (with employees).
- `POST /api/Department` — Create a new department (**Admin only**).
- `PUT /api/Department/{id}` — Update a department (**Admin only**).
- `DELETE /api/Department/{id}` — Delete a department (**Admin only**).

### Employees

- `GET /api/Employee` — List all employees (with departments).
- `GET /api/Employee/{id}` — Get an employee by ID (with department).
- `POST /api/Employee` — Create a new employee.
- `PUT /api/Employee/{id}` — Update an employee.
- `DELETE /api/Employee/{id}` — Delete an employee.

---

## 🔒 Security

- All endpoints require authentication via JWT Bearer tokens.
- Sensitive operations (department creation, update, delete) require the `Admin` role.
- Use the `/api/Account/Login` endpoint to obtain a JWT token and include it in the `Authorization` header as `Bearer <token>`.

---

## 📁 Project Structure

- `Controllers/` — API controllers for authentication, departments, and employees.
- `Model/` — Entity and DTO classes.
- `Interfaces/` — Repository interfaces.
- `Reposatories/` — Repository implementations.
- `Utils/` — Utility classes (e.g., JWT token generation).
- `Program.cs` — Application entry point and service configuration.

---

## 📄 License

This project is licensed under the MIT License.

---

**OrgManager-API** provides a robust foundation for organizational management systems with secure, role-based access and extensible architecture. 🌟

