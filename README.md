# POS Banking System with Microservices

## Project Overview

This project implements a Point of Sale (POS) system using .NET 9.0 with Clean Architecture principles. The system is designed to ensure modularity, scalability, and maintainability by leveraging a microservices-based architecture.

## Core Modules

- **User Management**: Create and manage users, associate accounts, and validate inputs.
- **Account Management**: Manage user accounts, including balances and account details.
- **Transaction Management**: Handle fund transfers and record transactions.

## Technology Stack

- **Framework**: .NET 9.0
- **Database**: SQL Server
- **ORM**: Dapper
- **Communication**: Azure Service Bus
- **Validation**: FluentValidation
- **Mediator Pattern**: MediatR
- **Error Handling**: ErrorOr
- **Unit of Work**: Custom implementation
- **API Gateway**: Ocelot

## Features and Workflows

### User Management
- **Create User**: Validate inputs and store user details.
- **Associate Accounts**: Link accounts to users.

### Account Management
- **Add Account**: Create new accounts with initial balances.
- **Update Balance**: Modify account balances.

### Transaction Management
- **Transfer Funds**: Perform debit and credit operations between accounts.
- **Log Transactions**: Maintain transaction records.

### Communication
- Services communicate asynchronously using Azure Service Bus for events like fund transfers and transaction logging.

## Architecture

### Clean Architecture Layers
1. **Presentation Layer**: API controllers via Ocelot API Gateway.
2. **Application Layer**: Business logic using MediatR commands and queries.
3. **Domain Layer**: Core business entities and domain logic.
4. **Infrastructure Layer**: Database interactions with Dapper, UnitOfWork, and Azure Service Bus integration.

### Microservices
- **User Management Service**: Handles user creation and management.
- **Account Management Service**: Manages accounts and balances.
- **Transaction Management Service**: Records and manages transactions.

## Endpoints

### User Management Service
- `POST /users/register`: Create a new user.
- `GET /users?Id=`: Retrieve user details.
- `POST /auth/login`: Get Token and Refreshtoken.
- `POST /users/refreshToken`: Get Refreshtoken.

### Account Management Service
- `POST /accounts/CreateAccount`: Add a new account.
- `PATCH /accounts/UpdateBalance`: Update account balance.
- `GET /accounts/GetAccount`: Retrieve account details.

### Transaction Management Service
- `POST /transactions/TransferFunds`: Perform fund transfers.
- `GET /transactions/getTransactionsByAccountId?AccountId`: Retrieve transaction details.

## Deployment

- **Containerization**: Dockerized microservices.
- **Hosting**: Deployed on Azure Kubernetes Service (AKS).
- **API Gateway**: Hosted as a separate service.

## Testing

- **Unit Tests**: Validate individual components.
- **Integration Tests**: Ensure inter-service communication.
- **Load Testing**: Assess scalability.

## Conclusion

This POS system architecture provides a robust and scalable solution for banking operations. The system ensures high performance and maintainability by adhering to Clean Architecture principles and utilizing modern technologies.

---

