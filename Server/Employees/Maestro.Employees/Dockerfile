FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Server/Employees/Maestro.Employees/Maestro.Employees.csproj", "Server/Employees/Maestro.Employees/"]
COPY ["Server/Server.Core/Server.Core.csproj", "Server/Server.Core/"]
COPY ["Core/Maestro.Core/Maestro.Core.csproj", "Core/Maestro.Core/"]
RUN dotnet restore "Server/Employees/Maestro.Employees/Maestro.Employees.csproj"
COPY . .
WORKDIR "/src/Server/Employees/Maestro.Employees"
RUN dotnet build "Maestro.Employees.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maestro.Employees.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maestro.Employees.dll"]