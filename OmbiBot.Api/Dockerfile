FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OmbiBot.Api/OmbiBot.Api.csproj", "OmbiBot.Api/"]
RUN dotnet restore "OmbiBot.Api/OmbiBot.Api.csproj"
COPY . .
WORKDIR "/src/OmbiBot.Api"
RUN dotnet build "OmbiBot.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OmbiBot.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OmbiBot.Api.dll"]