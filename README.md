# Inventory Service

> [!IMPORTANT]
> This solution is part of the Inventory Management System and should be run together with the rest of the services. The whole system is composed of the following components:
> - [API Gateway](https://github.com/claurymr/store-api) <--- Complete setup instructions can be found here.
> - [Inventory Auth Service](https://github.com/claurymr/inventory-auth-service) (this repository)
> - [Product Service](https://github.com/claurymr/product-service) 
> - [Inventory Service](https://github.com/claurymr/inventory-service) 

## Overview
The Inventory Auth Service is responsible for managing authentication and authorization within the Inventory Management System. It ensures that only authorized users can access and perform actions on the system's resources. This service handles the generation and validation of JWT tokens, providing a secure mechanism for user authentication. It is designed to work seamlessly with other components of the system, such as the API Gateway, Product Service, and Inventory Service.

This solution uses Docker for containerization, making it easy to run on macOS, Linux, and Windows.

## Development Approach Decisions

### FastEndpoints for Minimal APIs
I used FastEndpoints to set up minimal APIs in a structured and efficient manner. FastEndpoints aligns well with the CQRS pattern, providing a clean and organized way to define API endpoints.

### FluentValidation for Request Data Validation
To ensure the validity of incoming requests, I implemented FluentValidation. This library provides a fluent interface for defining validation rules, making it easy to enforce data integrity and provide meaningful error messages.

### Global Exception Handling
I implemented a simple global exception handling to manage errors consistently across the application and avoid raw exception messages to be returned in the response. This approach ensures that all exceptions are caught and handled gracefully, providing a better user experience and easier debugging.

### JWT Auth
To handle authentication and authorization, I implemented a simple JWT Auth by setting up predetermined credentials that don't need to be saved to a database. This decision was taken to simplify the process of authentication and authorization while prioritizing other functionalities, as registration and login were not required for the nature of this project. This approach allows for secure access control without the overhead of managing user credentials in a database.