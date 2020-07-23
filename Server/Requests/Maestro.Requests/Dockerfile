FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Server/Requests/Maestro.Requests/Maestro.Requests.csproj", "Server/Requests/Maestro.Requests/"]
COPY ["Server/Server.Core/Server.Core.csproj", "Server/Server.Core/"]
COPY ["Core/Maestro.Core/Maestro.Core.csproj", "Core/Maestro.Core/"]
RUN dotnet restore "Server/Requests/Maestro.Requests/Maestro.Requests.csproj"
COPY . .
WORKDIR "/src/Server/Requests/Maestro.Requests"
RUN dotnet build "Maestro.Requests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maestro.Requests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maestro.Requests.dll"]