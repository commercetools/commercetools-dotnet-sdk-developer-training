# commercetools-dotnet-sdk-developer-training

A training project demonstrating the commercetools .NET SDK in a fullstack setup with the Backend-for-Frontend (BFF) and a lightweight frontend built with **HTML, CSS, and JavaScript**.

## 🎯 Goals

- Learn how to use the commercetools .NET SDK in a backend service.
- Explore a simple UI that interacts with the BFF to trigger commercetools API calls.
- Practice realistic commerce use cases like product listing, cart management, and checkout flow.

## 🗂️ Project Structure

- **Backend**: ASP .NET Core
- **Frontend**: Lightweight UI using HTML, CSS, and JavaScript for interacting with the backend.
- **commercetools SDK**: Demonstrates integrations with commercetools’ APIs.

## Features

- Provides a basic BFF setup for interacting with commercetools APIs.
- Includes a simple frontend for triggering e-commerce actions (e.g., product browsing, cart management, and checkout).
- Full-stack application structure to demonstrate how to use the commercetools .NET SDK effectively.

## Getting Started

### Setup API Client in Merchant Center

Before running the project, you need to create an API client in the commercetools Merchant Center and provide its credentials in the `appsettings.Development.json` file.

1. Go to the [commercetools Merchant Center](https://mc.europe-west1.gcp.commercetools.com/).
2. Create a new API client:
   - Navigate to **Project settings** > **API clients**.
   - Click on **Create API Client** and select Admin client template from drop down list.
   - Make a note of the **Project Key**, **Client ID**, and **Client Secret**.

### Installation

```bash
$ dotnet build
```

### Configure Environment Variables

The environment variables need to be set in the `appsettings.Development.json` file under a section "Commercetools". You will also need to create a section "Import" for Import API specific credentials.

Example `appsettings.Development.json` file:

```
"Commercetools": {
    "ProjectKey": "your_project_key",
    "ClientId": "your_client_id",
    "ClientSecret": "your_client_secret",
    "ApiBaseAddress": "https://api.europe-west1.gcp.commercetools.com/",
    "AuthorizationBaseAddress": "https://auth.europe-west1.gcp.commercetools.com/"
},
"Import": {
    "ProjectKey": "your_project_key",
    "ClientId": "your_client_id",
    "ClientSecret": "your_client_secret",
    "ApiBaseAddress": "https://import.europe-west1.gcp.commercetools.com/",
    "AuthorizationBaseAddress": "https://auth.europe-west1.gcp.commercetools.com/"
}
```

### Running the Project

For development:

```bash
$ dotnet run
```

For watching file changes in development:

```bash
$ dotnet watch run
```

### Frontend

For viewing the frontend, plese go to http://localhost:8080

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/nestjs/nest/blob/master/LICENSE) file for details.