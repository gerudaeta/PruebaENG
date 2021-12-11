using FluentValidation;

namespace PruebaENG.Application.Users.Commands.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The name field is required.");
        
        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("The BirthDate field is required.");
    }
}