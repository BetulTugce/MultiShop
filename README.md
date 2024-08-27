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
    "MSSQLServerConnection": "Server=your_server_name;Database=MultiShopOrderDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;"
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
