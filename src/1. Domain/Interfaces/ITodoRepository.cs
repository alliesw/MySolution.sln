namespace Domain.Interfaces;

using Domain.Entities;

public interface ITodoRepository
{
  Task<Todo?> GetByIdAsync(Guid id, CancellationToken ct = default);
  Task<IEnumerable<Todo>>
    GetAllSync(CancellationToken ct = default);
  void Add(Todo, todo);
  void Update(Todo, todo);
  void Delete (Todo, todo);
  Task<int>
SaveChangesAsync(CancellationToken ct = default);
}
