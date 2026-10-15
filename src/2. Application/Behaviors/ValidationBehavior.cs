namespace Application.Behaviors; 

using ;
using MediatR;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : notnull
{
  public async Task<TResponse> Handle(
