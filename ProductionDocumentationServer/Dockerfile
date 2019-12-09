FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ProductionDocumentationServer/ProductionDocumentationServer.csproj", "ProductionDocumentationServer/"]
RUN dotnet restore "ProductionDocumentationServer/ProductionDocumentationServer.csproj"
COPY . .
WORKDIR "/src/ProductionDocumentationServer"
RUN dotnet build "ProductionDocumentationServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductionDocumentationServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductionDocumentationServer.dll"]