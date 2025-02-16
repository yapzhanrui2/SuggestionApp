# Ticket Management Web Application

This repository contains a **Ticket Management Web Application**, implemented based on the C# Course by **Tim Corey**.

## Technologies Used

- **Blazor Server (.NET)** – For building interactive web applications.
- **MongoDB + Atlas** – NoSQL database for data storage and management.
- **Azure Active Directory B2C** – Authentication and authorization solution.
- **Bootstrap** – Responsive UI framework for styling and layout.
- **Authentication/Authorization** – Secure user management.
- **CSS** – Custom styles for enhanced user experience.

## Features

- **User Authentication**: Secure login and registration using Azure AD B2C.
- **Role-Based Authorization**: Access control for different user roles.
- **Ticket Management**: Create, update, and resolve support tickets.
- **Real-Time Data Handling**: Efficient data updates using MongoDB Atlas.
- **Responsive UI**: Mobile-friendly and accessible design using Bootstrap.

## Getting Started

Follow these steps to set up and run the project locally:

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd <repository-folder>
   ```

2. **Set up MongoDB Atlas**:
   - Create a MongoDB Atlas account.
   - Configure your database cluster and obtain a connection string.
   - Update the `appsettings.json` file with your MongoDB connection string.

3. **Configure Azure AD B2C**:
   - Register an application in **Azure Active Directory B2C**.
   - Set up user flows and policies for authentication.
   - Update the application settings with **Client ID** and **Tenant Information**.

4. **Run the application**:
   ```bash
   dotnet run
   ```
   The application should now be running at `https://localhost:5001/`.

## Contributing

Contributions are welcome! If you have suggestions, feel free to open an issue or submit a pull request.

