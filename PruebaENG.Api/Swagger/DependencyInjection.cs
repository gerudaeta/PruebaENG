using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PruebaENG.Api.Swagger;

public static class DependencyInjection
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PruebaENG.Api",
                Description = "Web API de Prueba"
            });
            swagger.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
        });

        return services;
    }
}