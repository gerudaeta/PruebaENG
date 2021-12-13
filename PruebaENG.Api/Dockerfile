#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PruebaENG.Api/PruebaENG.Api.csproj", "PruebaENG.Api/"]
COPY ["PruebaENG.Application/PruebaENG.Application.csproj", "PruebaENG.Application/"]
COPY ["PruebaENG.Domain/PruebaENG.Domain.csproj", "PruebaENG.Domain/"]
COPY ["PruebaENG.Infrastructure/PruebaENG.Infrastructure.csproj", "PruebaENG.Infrastructure/"]
RUN dotnet restore "PruebaENG.Api/PruebaENG.Api.csproj"
COPY . .
WORKDIR "/src/PruebaENG.Api"
RUN dotnet build "PruebaENG.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PruebaENG.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PruebaENG.Api.dll"]