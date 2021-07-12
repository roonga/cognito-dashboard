FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CognitoDashboard/CognitoDashboard.csproj", "CognitoDashboard/"]
RUN dotnet restore "CognitoDashboard/CognitoDashboard.csproj"
COPY . .
WORKDIR "/src/CognitoDashboard"
RUN dotnet build "CognitoDashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CognitoDashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CognitoDashboard.dll"]