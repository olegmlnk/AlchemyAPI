# AlchemyAPI

AlchemyAPI is a backend service designed to manage appointments, masters (service providers), services offered, and user authentication for a scheduling platform. It provides a RESTful API for various operations related to these entities.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Database](#database)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Authentication](#authentication)
  - [Appointments](#appointments)
  - [Masters](#masters)
  - [Master Schedule](#master-schedule)
  - [Services](#services)
- [Authentication and Authorization](#authentication-and-authorization)
- [Error Handling and Validation](#error-handling-and-validation)

## Features

* **User Management:**
    * User registration with email and password.
    * User login with JWT-based authentication.
    * (Partially implemented/Planned) External login with Google.
    * Role-based authorization (Admin, User/Client).
* **Appointment Management:**
    * Create, view, update, and cancel appointments.
    * Link appointments to users, masters, services, and schedule slots.
* **Master (Service Provider) Management:**
    * CRUD operations for masters (name, experience, description).
* **Service Management:**
    * CRUD operations for services (title, description, price, duration).
* **Master Schedule Management:**
    * View master schedules and individual slots.
    * Check slot availability.
    * Book and free up schedule slots.
* **Database:**
    * Uses Entity Framework Core for ORM.
    * Database migrations for schema management.
* **API Documentation:**
    * Swagger (OpenAPI) integration for API exploration and testing.

## Technologies Used

* **.NET / ASP.NET Core:** Framework for building the API.
* **Entity Framework Core:** Object-Relational Mapper (ORM) for database interactions.
* **SQL Server:** Relational database system.
* **JWT (JSON Web Tokens):** For secure stateless authentication.
* **ASP.NET Core Identity:** For user management and authentication.
* **AutoMapper:** For object-to-object mapping.
* **Swagger/OpenAPI:** For API documentation and testing.
* **Serilog (implied by ILogger):** For logging.

## Project Structure

The project follows a clean architecture pattern, separating concerns into different layers:

* `Alchemy.Domain`: Contains core domain models, interfaces for repositories and services, and domain-specific business logic.
* `Alchemy.Application`: Implements the application services (use cases) by orchestrating domain models and repositories. It acts as a bridge between the API layer and the domain layer.
* `Alchemy.Infrastructure`: Provides implementations for interfaces defined in the Domain layer, such as repositories (data access), JWT handling, and other external concerns like database context and migrations.
* `AlchemyAPI`: The presentation layer, containing API controllers, request/response Data Transfer Objects (DTOs/Contracts), and the application's entry point (`Program.cs`).
* `.idea`: Contains project-specific settings for JetBrains IDEs.

## Setup and Installation

### Prerequisites

* .NET SDK (Version compatible with the project, likely .NET 7 or newer based on `Program.cs` structure and C# features)
* SQL Server instance

### Configuration

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd AlchemyAPI
    ```
2.  **Database Connection String:**
    Update the `Default` connection string in `AlchemyAPI/appsettings.json` to point to your SQL Server instance:
    ```json
    "ConnectionStrings": {
      "Default": "Data Source=YOUR_SERVER_NAME;Initial Catalog=AlchemyApiDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True" // Or your specific connection string
    },
    ```
   
3.  **JWT Configuration:**
    Review and update JWT settings in `AlchemyAPI/appsettings.json` if necessary. The `SecretKey` is crucial for token signing.
    ```json
    "Jwt": {
      "Issuer": "AlchemyAPI",
      "Audience": "AlchemyClient",
      "SecretKey": "YOUR_VERY_SECRET_KEY_HERE_SHOULD_BE_STRONG", // Replace with a strong, unique key
      "ExpiresHours": "12"
    }
    ```
   
4.  **Google Authentication (Optional):**
    If you plan to use Google external login, update `ClientId` and `ClientSecret` in `AlchemyAPI/appsettings.json`.
    ```json
    "Google": {
      "ClientId": "YOUR_GOOGLE_CLIENT_ID",
      "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
    }
    ```
   

### Database

The project uses Entity Framework Core migrations to manage the database schema.

1.  **Ensure EF Core tools are installed:**
    If not already installed, you might need to install them globally or as a local tool.
    ```bash
    dotnet tool install --global dotnet-ef
    # or for local tool (preferred if project has a tool manifest)
    # dotnet tool install dotnet-ef
    ```
2.  **Apply Migrations:**
    Navigate to the `AlchemyAPI` project directory (where the `.csproj` file is located) in your terminal and run:
    ```bash
    dotnet ef database update --project ../Alchemy.Infrastructure/Alchemy.Infrastructure.csproj --startup-project ./AlchemyAPI.csproj
    ```
    This will create the database (`AlchemyApiDB` by default) and apply all pending migrations.

### Running the Application

1.  **Restore Dependencies:**
    ```bash
    dotnet restore
    ```
2.  **Build the Application:**
    ```bash
    dotnet build
    ```
3.  **Run the Application:**
    Navigate to the `AlchemyAPI` project directory and run:
    ```bash
    dotnet run
    ```
    By default, the application will be accessible at `https://localhost:7215` and `http://localhost:5292` (check `Properties/launchSettings.json`).
    The Swagger UI will be available at the root URL (e.g., `https://localhost:7215`).

## API Endpoints

The API provides the following main groups of endpoints. Refer to the Swagger UI for detailed request/response models and to try them out.

### Authentication (`/api/Auth`)

* `POST /register`: Registers a new user.
* `POST /login`: Logs in an existing user and returns a JWT token.
* *(Partially implemented) `POST /external-login`: Handles login via external providers like Google.*
* *(Partially implemented) `POST /forgot-password`, `POST /ResetPassword`.*

### Appointments (`/api/`)

* `GET /GetAllAppointments`: Retrieves all appointments.
* `GET /GetAppointmentById?id={id}`: Retrieves a specific appointment by its ID.
* `POST /CreateAppointment`: Creates a new appointment. Requires a valid `ScheduleSlotId`.
* `PUT /UpdateAppointment?id={id}`: Updates an existing appointment.
* `DELETE /CancelAppointment?id={id}`: Cancels an appointment and marks the corresponding schedule slot as available.

### Masters (`/api/Master`)

* `GET /GetAll`: Retrieves all masters.
* `POST /Create`: Creates a new master. (Admin role required)
* `PUT /Update/{id}`: Updates an existing master. (Admin role required)
* `DELETE /Delete/{id}`: Deletes a master. (Admin role required)

### Master Schedule (`/api/MasterSchedule`)

* `GET /`: Retrieves all master schedule slots.
* `GET /{id}`: Retrieves a specific schedule slot by ID. (Admin role required for some operations)
* `GET /master/{masterId}`: Retrieves all schedule slots for a specific master.
* `GET /{id}/available`: Checks if a specific schedule slot is available.
* `PUT /{id}/book`: Marks a schedule slot as booked.
* `PUT /{id}/free`: Marks a schedule slot as available. (Admin role required)

### Services (`/api/Services`)

* `GET /`: Retrieves all services.
* `GET /GetById{id}`: Retrieves a specific service by its ID.
* `POST /Create`: Creates a new service. (Admin role likely intended/required for full management)
* `PUT /Update{id}`: Updates an existing service. (Admin role likely intended/required)
* `DELETE /Delete{id}`: Deletes a service. (Admin role likely intended/required)

## Authentication and Authorization

* **JWT Authentication:** The API uses JSON Web Tokens (JWT) for authenticating requests after a user logs in. The token should be included in the `Authorization` header of subsequent requests as a Bearer token.
* **Roles:**
    * **Admin:** Has full access to all resources, including management of masters, services, and all user/appointment data.
    * **User/Client:** Has access to their own data, booking appointments, etc.
    * Specific endpoints are protected by `[Authorize]` attributes, sometimes with specific roles (e.g., `[Authorize(Roles = "Admin")]`).

## Error Handling and Validation

* Standard HTTP status codes are used to indicate the success or failure of API requests.
* Input validation is performed on request models (e.g., using DataAnnotations in contracts and model validation in domain entities).
* Descriptive error messages are returned in the response body for failed requests (e.g., validation errors, "Not Found" exceptions).# AlchemyAPI