FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["GrpcSongService/GrpcSongService.csproj", "GrpcSongService/"]
RUN dotnet restore "GrpcSongService/GrpcSongService.csproj"
COPY . .
WORKDIR "/src/GrpcSongService"
RUN dotnet build "GrpcSongService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcSongService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcSongService.dll"]