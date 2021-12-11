using FluentValidation;
using PruebaENG.Application.Common.Interfaces;

namespace PruebaENG.Application.Common.Validators;

public static class PaginacionValidator
{
    public static IRuleBuilderOptions<T, IPaginacion> PaginacionValida<T>(this IRuleBuilder<T, IPaginacion> ruleBuilder)
    {
        return ruleBuilder
            .Must(x =>
            {
                if (x.PageNumber <= 0)
                {
                    return false;
                }

                return true;
            }).WithMessage("El número de página debe ser mayor o igual a 1.")
            .Must(x =>
            {
                if (x.PageSize <= 0)
                {
                    return false;
                }

                return true;
            }).WithMessage("El tamaño de página debe ser mayor o igual a 1.");
    }
}