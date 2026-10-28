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




