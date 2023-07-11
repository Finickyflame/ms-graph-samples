# Microsoft Graph samples

Samples applications using the Microsoft Graph Sdk and Dotnet 7

## Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/), [Visual Studio Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)

## Projects
- [ConsoleApp](./src/ConsoleApp)

## How to debug

### 1. Restore projects
```bash
dotnet restore
```

### 2. Update appsettings.json with your own Tenant, Client and Secret Ids
```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "00000000-0000-0000-0000-000000000000",
    "ClientId": "00000000-0000-0000-0000-000000000000",
    "ClientCredentials": [
      {
        "SourceType": "ClientSecret",
        "ClientSecret": "00000000-0000-0000-0000-000000000000-"
      }
    ]
  }
}
```

See [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/en-ca/azure/active-directory/develop/quickstart-register-app) for more details on how to register an app on Azure Active Directory.

Your registered application in Azure will need both `User.ReadWrite.All` and `Tasks.ReadWrite.All` permissions in order for this executable to work properly.


### 3. Run the application
```bash
dotnet run
```

## Links
- [Microsoft Graph REST API v1.0 endpoint reference](https://learn.microsoft.com/en-us/graph/api/overview?view=graph-rest-1.0)
- [Daemon app that calls web APIs - app registration](https://learn.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-app-registration)
- [Azure Samples - Daemon](https://github.com/Azure-Samples/active-directory-dotnetcore-daemon-v2/tree/master)