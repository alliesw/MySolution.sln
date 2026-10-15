namespace Domain.Events;

public record TodoCompletedEvent(Guid, Todold, DateTime CompletedAtUtc);
