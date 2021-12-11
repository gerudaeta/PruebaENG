using FluentValidation;
using PruebaENG.Application.Common.Validators;

namespace PruebaENG.Application.Users.Queries.GetUsersActiveWithPaginationRequest;

public class GetUsersActiveWithPaginationRequestValidator : AbstractValidator<GetUsersActiveWithPaginationRequest>
{
    public GetUsersActiveWithPaginationRequestValidator()
    {
        RuleFor(x => x).PaginacionValida();
    }
}