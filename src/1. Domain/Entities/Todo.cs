namespace Domain.Entities;

using Domain.Events;

public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private readonly List<object> _domainEvents = [];
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private Todo() { } // EF Core

    public Todo(string title)
    {
        Id = Guid.NewGuid();
        Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Title required", nameof(title)) : title.Trim();
        IsCompleted = false;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (IsCompleted) return;
        IsCompleted = true;
        _domainEvents.Add(new TodoCompletedEvent(Id, DateTime.UtcNow));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
Use code with caution.


ValueObjects/Money.cs

csharp
namespace Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public static Money Zero(string currency = "USD") => new(0m, currency);
    public Money Add(Money other)
    {
        if (Currency != other.Currency) throw new InvalidOperationException("Currency mismatch");
        return new Money(Amount + other.Amount, Currency);
    }
}
Use code with caution.


Events/TodoCompletedEvent.cs

csharp
namespace Domain.Events;

public record TodoCompletedEvent(Guid TodoId, DateTime CompletedAtUtc);
Use code with caution.


Interfaces/ITodoRepository.cs

csharp
namespace Domain.Interfaces;

using Domain.Entities;

public interface ITodoRepository
{
    Task<Todo?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken ct = default);
    void Add(Todo todo);
    void Update(Todo todo);
    void Delete(Todo todo);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
Use code with caution.




2. Application (src/2. Application/)

Todos/Commands/CreateTodoCommand.cs

csharp
namespace Application.Todos.Commands;

using MediatR;

public record CreateTodoCommand(string Title) : IRequest<Guid>;
Use code with caution.


Todos/Commands/CreateTodoCommandHandler.cs

csharp
namespace Application.Todos.Commands;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class CreateTodoCommandHandler(ITodoRepository repository) : IRequestHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken ct)
    {
        var todo = new Todo(request.Title);
        repository.Add(todo);
        await repository.SaveChangesAsync(ct);
        return todo.Id;
    }
}
Use code with caution.


Todos/Validators/CreateTodoValidator.cs

csharp
namespace Application.Todos.Validators;

using Application.Todos.Commands;
using FluentValidation;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
    }
}
Use code with caution.
