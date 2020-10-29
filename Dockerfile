FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["VideoClubApiRest.Api/VideoClubApiRest.Api.csproj", "VideoClubApiRest.Api/"]
COPY ["VideoClubApiRest.Core/VideoClubApiRest.Core.csproj", "VideoClubApiRest.Core/"]
COPY ["VideoClubApiRest.Infraestructure/VideoClubApiRest.Infraestructure.csproj", "VideoClubApiRest.Infraestructure/"]
RUN dotnet restore "VideoClubApiRest.Api/VideoClubApiRest.Api.csproj"
COPY . .
WORKDIR "/src/VideoClubApiRest.Api"
RUN dotnet build "VideoClubApiRest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VideoClubApiRest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoClubApiRest.Api.dll"]