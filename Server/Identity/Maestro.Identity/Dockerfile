FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Maestro.Identity/Maestro.Identity.csproj", "Identity/Maestro.Identity/"]
COPY ["../Server.Core/Server.Core.csproj", "Server/Server.Core/"]
COPY ["Core/Maestro.Core/Maestro.Core.csproj", "Core/Maestro.Core/"]
RUN dotnet restore "Server/Identity/Maestro.Identity/Maestro.Identity.csproj"
COPY . .
WORKDIR "/src/Server/Identity/Maestro.Identity"
RUN dotnet build "Maestro.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maestro.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maestro.Identity.dll"]