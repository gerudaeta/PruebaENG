using FluentValidation.TestHelper;
using PruebaENG.Application.Users.Commands.CreateUser;
using PruebaENG.Domain.Entities;
using Xunit;

namespace PruebaENG.Application.UnitTests.Users.Commands;

public class CreateUserRequestValidatorTests
{
    private readonly CreateUserRequestValidator _validator;
    
    public CreateUserRequestValidatorTests()
    {
        _validator = new CreateUserRequestValidator();
    }

    [Fact]
    public void Validator_UserFieldNull_ReturnValidationError()
    {
        var request = new CreateUserRequest();

        var result = _validator.TestValidate(request);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.BirthDate);
    }
    
}