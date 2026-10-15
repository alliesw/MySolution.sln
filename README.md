MySolution.sln
│
├── 📁 src/
│   ├── 📁 1. Domain (Core Business Logic)
│   │   ├── Entities/          -> Pure business objects (Todo.cs)
│   │   ├── ValueObjects/      -> Immutable domain concepts (Money, Address)
│   │   ├── Events/            -> Domain events (TodoCompletedEvent.cs)
│   │   └── Interfaces/        -> Repository contracts (ITodoRepository.cs)
│   │
│   ├── 📁 2. Application (Use Cases / Business Rules)
│   │   ├── Todos/
│   │   │   ├── Commands/      -> Write actions (CreateTodoCommand.cs + Handler)
│   │   │   ├── Queries/       -> Read actions (GetTodosQuery.cs + Handler)
│   │   │   └── Validators/    -> FluentValidation rules (CreateTodoValidator.cs)
│   │   └── Behaviors/         -> MediatR pipeline cross-cutting concerns (Logging, Validation)
│   │
│   ├── 📁 3. Infrastructure (External Concerns / Data Access)
│   │   ├── Persistence/       -> EF Core DbContext, Migrations, Entity Configurations
│   │   ├── Repositories/      -> Concrete ITodoRepository implementation (using EF Core or Dapper)
│   │   ├── Services/          -> Email, Azure Blob Storage, Redis cache clients
│   │   └── DependencyInjection.cs -> Registers infra services
│   │
│   └── 📁 4. Presentation / WebApi (Entry Point)
│       ├── Endpoints/         -> Minimal API route mappings grouped by feature
│       ├── Middleware/        -> Global exception handler, security headers
│       └── Program.cs         -> Composition root (wires everything together)
│
└── 📁 tests/
    ├── 📁 Domain.UnitTests/
    ├── 📁 Application.UnitTests/
    └── 📁 Architecture.Tests/ -> Enforces layer dependency rules (NetArchTest)
