# Ecommerce API

A minimal ASP.NET Core Web API for basic e-commerce functionality, including product management, order handling, and JWT-based authentication.

## Features

- JWT Authentication for Customers
- Products CRUD endpoints
- Order placement and order history
- EF Core with SQL Server
- Swagger UI for API documentation

## Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Swagger/OpenAPI

## Endpoints Overview

### Authentication
- `POST /api/authentication/token`: Authenticate and get a JWT

### Products
- `GET /api/products/getproducts`: Get all products
- `POST /api/products/addproducts`: Add a new product
- `PUT /api/products/editproducts/{id}`: Update a product
- `DELETE /api/products/deleteproducts/{id}`: Delete a product

### Orders (requires auth)
- `GET /api/orders/history`: View order history for authenticated user
- `POST /api/orders/place`: Place a new order
- `PUT /api/orders/update/{id}`: Update order status

## Getting Started

1. Clone the repo
2. Update the `appsettings.json` connection string and JWT config
3. Run migrations and start the API:
   ```bash
   dotnet ef database update
   dotnet run
4. Access Swagger at `https://localhost:<port>/swagger`

License
MIT License – see LICENSE file for details.

