# MultiShop

## Overview

This project is an e-commerce application currently being developed with ASP.NET Core. It currently includes three microservices: **Catalog**, **Discount** and **Order**. 
I will add more microservices in the future to make the project better and bigger.

### Microservices

#### Catalog Microservice

- **Database**: MongoDB
- **Features**: Some CRUD operations for categories and products.

**Packages**:
- [MongoDB.Bson](https://www.nuget.org/packages/MongoDB.Bson/) v2.28.0
- [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver/) v2.28.0
- [AutoMapper](https://www.nuget.org/packages/AutoMapper/) v13.0.1
- [SwashBuckle.AspNetCore](https://www.nuget.org/packages/SwashBuckle.AspNetCore/) v6.4.0

**Configuration (appsettings.json)**:
```json
{
  "DatabaseSettings": {
    "CategoryCollectionName": "Categories",
    "ProductCollectionName": "Products",
    "ProductDetailCollectionName": "ProductDetails",
    "ProductImageCollectionName": "ProductImages",
    "ConnectionString": "your_connection_string",
    "DatabaseName": "MultiShopCatalogDb"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
*Note: Replace **your_connection_string** with your MongoDB server connection string*

**Connection String Examples:**

- **Local Server (localhost)**: `mongodb://localhost:27017`  
  This is the default connection string for a MongoDB server running on your local machine.

- **Remote Server**: `mongodb://your_mongodb_server:27017`  
  Replace `your_mongodb_server` with the IP address or domain name of your remote MongoDB server. This example assumes the default port is used.

- **Custom Port**: `mongodb://your_mongodb_server:your_custom_port`  
  If your MongoDB server uses a port other than the default (27017), replace `your_custom_port` with the actual port number.

*********

#### Discount Microservice

- **Database**: MS SQL Server
- **Features**: Some CRUD operations for coupons.

**Packages**:
- [Dapper](https://www.nuget.org/packages/Dapper/) v2.1.35
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) v8.0.8
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/) v8.0.8
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) v8.0.8
- [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/) v8.0.8
- [SwashBuckle.AspNetCore](https://www.nuget.org/packages/SwashBuckle.AspNetCore/) v6.4.0

**Configuration (appsettings.json)**:

The `appsettings.json` file is not included in the project as it contains sensitive information. Instead, you should create your own `appsettings.json` file with the following structure for MultiShop.Discount project:

```json
{
  "ConnectionStrings": {
    "MSSQLServerConnection": "Server=your_server_name;Database=MultiShopDiscountDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
*Note: Replace **your_server_name** with your actual SQL Server instance name.*

*********

#### Order Microservice

- **Database**: MS SQL Server
- **Features**: Utilizes Onion architecture, CQRS and Mediator design patterns. Includes various CRUD operations for managing orders.

**Packages**:

- **Core (Application)**:
  - [MediatR](https://www.nuget.org/packages/MediatR/) v12.4.0
  - [Microsoft.Extensions.Configuration.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Abstractions/8.0.0) v8.0.0
- **Infrastructure (Persistence)**:
  - [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) v8.0.8
  - [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/) v8.0.8
  - [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) v8.0.8
- **Presentation (WebApi)**:
  - [MediatR](https://www.nuget.org/packages/MediatR/) v12.4.0
  - [Swashbuckle.AspNetCore](https://www.nuget.org/packages/SwashBuckle.AspNetCore/) v6.4.0

**Configuration (appsettings.json)**:

The `appsettings.json` file is not included in the project as it contains sensitive information. Instead, you should create your own `appsettings.json` file with the following structure for MultiShop.Order.WebApi project:

```json
{
  "ConnectionStrings": {
    "MSSQLServerConnection": "Server={your_server};Database={your_database};User={your_username};Password={your_password};Trusted_Connection=False;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
*Note: Replace **{your_server}**, **{your_database}**, **{your_username}**, and **{your_password}** with your actual SQL Server connection details.*

*********

#### IdentityServer Microservice

- **Database**: MS SQL Server

**Packages**:
- [IdentityServer4.AspNetIdentity](https://www.nuget.org/packages/IdentityServer4.AspNetIdentity/4.1.2/) v4.1.2
- [Microsoft.AspNetCore.Authentication.Google](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Google/8.0.8) v8.0.8
- [Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore/8.0.8) v8.0.8
- [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/8.0.8) v8.0.8
- [Microsoft.AspNetCore.Identity.UI](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.UI/8.0.8) v8.0.8
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/) v8.0.8
- [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/) v8.0.8
- [Serilog.AspNetCore](https://www.nuget.org/packages/Serilog.AspNetCore/8.0.2) v8.0.2

**Configuration (appsettings.json)**:

The `appsettings.json` file is not included in the project as it contains sensitive information. Instead, you should create your own `appsettings.json` file with the following structure for MultiShop.IdentityServer project:

```json
{
  "ConnectionStrings": {
    "MSSQLServerConnection": "Server={your_server};Database={your_database};User={your_username};Password={your_password};Trusted_Connection=False;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;"
  },
  "CertificateSettings": {
    "Password": "{cert_password}"
  }
}

```
*Note: Replace **{your_server}**, **{your_database}**, **{your_username}**, and **{your_password}** with your actual SQL Server connection details. Replace **{cert_password}** with your actual certificate password.*

**X.509 Certificate**

In IdentityServer4, the `AddDeveloperSigningCredential()` method creates a temporary signing key for development. This is fine for local development, but in production, you need a more secure approach. Using an X.509 certificate is one option.

For production, you need to either generate or buy an X.509 certificate from a Certificate Authority (CA). But for development, you can create a self-signed certificate. Here’s how to do it using PowerShell.

**Create a Self-Signed Certificate with PowerShell**

1. To generate a self-signed certificate in PowerShell, run the following commands:

   ```powershell
   $cert = New-SelfSignedCertificate -DnsName "localhost" -CertStoreLocation "cert:\LocalMachine\My" -KeySpec KeyExchange
   ```
   `-DnsName "localhost":` Specifies the DNS name for the certificate. You can replace this with your domain, e.g., `yourdomain.com.`

2. Create a password for the certificate:

  ```powershell
   $certPassword = ConvertTo-SecureString -String "your_password_here" -Force -AsPlainText
   ```
  This command creates a password to protect the `.pfx` file. Replace `your_password_here` with your chosen password.

3. Export the certificate to a .pfx file:

  ```powershell
   Export-PfxCertificate -Cert $cert -FilePath "C:\path_to_your_certificate.pfx" -Password $certPassword
   ```

*Note: Run PowerShell as an administrator if you get an `access denied` error.*

**Adding the Certificate to the Project**

After generating the self-signed certificate, follow these steps to integrate it into your project:

1. **Move the Certificate:**

   -Move the `.pfx` file to the `Certificates` folder in the `MultiShop.IdentityServer` project.

2. **Update appsettings.json**

   -Store the certificate password in appsettings.json:

    ```json
    {
      "CertificateSettings": {
        "Password": "{cert_password}"
      }
    }
    ```
3. **Update Startup.cs**

   -Modify the `Startup.cs` file to load and use the certificate:

    ```csharp
    namespace MultiShop.IdentityServer
    {
        public class Startup
        {
            ...

            public void ConfigureServices(IServiceCollection services)
            {
                ...

                // The certificate is loaded from the "Certificates" folder in the project's root directory
                var certificatePath = Path.Combine(Directory.GetCurrentDirectory(), "Certificates", "MultiShopCertificate.pfx");
                var certificatePassword = Configuration["CertificateSettings:Password"];

                // Loading the certificate
                var certificate = new X509Certificate2(certificatePath, certificatePassword);

                ...

                // not recommended for production - you need to store your key material somewhere secure
                // builder.AddDeveloperSigningCredential();

                // Use the X.509 certificate to sign JWT tokens
                builder.AddSigningCredential(certificate);

                ...
            }
        }
    }
    ```

**Developer Signing Credential (For Development Only)**

For the development environment, using an X.509 certificate is not mandatory. The `builder.AddDeveloperSigningCredential();` method is sufficient. This method generates a temporary signing key, allowing developers to work quickly without needing a certificate. However, this is not suitable for production due to security concerns. Creating a self-signed certificate using PowerShell helps simulate a more secure signing method during development. Even though self-signed certificates may not be fully secure, they prepare us for the transition to production and help us become familiar with security concepts.

In production environments, trusted certificates are necessary to ensure that browsers and clients recognize them as secure. Therefore, before going live, obtaining a certificate from a trusted Certificate Authority (CA) is the best practice.

*********
