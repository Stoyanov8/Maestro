FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Server/Statistics/Maestro.Statistics/Maestro.Statistics.csproj", "Server/Statistics/Maestro.Statistics/"]
COPY ["Server/Server.Core/Server.Core.csproj", "Server/Server.Core/"]
COPY ["Core/Maestro.Core/Maestro.Core.csproj", "Core/Maestro.Core/"]
RUN dotnet restore "Server/Statistics/Maestro.Statistics/Maestro.Statistics.csproj"
COPY . .
WORKDIR "/src/Server/Statistics/Maestro.Statistics"
RUN dotnet build "Maestro.Statistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maestro.Statistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maestro.Statistics.dll"]