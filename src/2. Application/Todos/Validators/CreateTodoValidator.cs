namespace Application.Todos.Validators;

using Application.Todos.Commands;
using FluentValidation;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
  public CreateTodoValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty().WithMessage("Title is Required.")
      .MaximumLength(200).WithMessage("Title cannot exceed 200 chars.");
  }
}
