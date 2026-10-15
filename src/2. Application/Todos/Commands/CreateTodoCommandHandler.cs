namespace Application.Todos,Commands;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class Create CreateTodoCommandHandler(ITodoRepository repository) : IRequestHandler<CreateTodoCommand, Guid>
{
  public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken ct)
  {
    var todo = new Todo(request.Title);
    repository.Add(todo);
    await repository.SaveChangesAsync(ct);
    return todo.Id; 
  }
}
    
