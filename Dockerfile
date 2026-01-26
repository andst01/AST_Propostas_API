# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os csproj para cache de restore
COPY ["src/Propostas.Api/Propostas.Api.csproj", "src/Propostas.Api/"]
COPY ["src/Propostas.Infra.CrossCuting/Propostas.Infra.CrossCuting.csproj", "src/Propostas.Infra.CrossCuting/"]
COPY ["src/Propostas.Infra.Data/Propostas.Infra.Data.csproj", "src/Propostas.Infra.Data/"]
COPY ["src/Propostas.Domain/Propostas.Domain.csproj", "src/Propostas.Domain/"]
COPY ["src/Propostas.Application/Propostas.Application.csproj", "src/Propostas.Application/"]

# Restaura pacotes
RUN dotnet restore "src/Propostas.Api/Propostas.Api.csproj"

# Copia todo o código
COPY . .

# Publica especificando **o caminho completo do csproj**
RUN dotnet publish "src/Propostas.Api/Propostas.Api.csproj" -c Debug -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Variáveis de ambiente para Development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:4090

# Copia os binários
COPY --from=build /app/publish .

EXPOSE 4091

ENTRYPOINT ["dotnet", "Propostas.Api.dll"]
