#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:6000
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SpaceService/SpaceService.csproj", "SpaceService/"]
RUN dotnet restore "SpaceService/SpaceService.csproj"
COPY . .
WORKDIR "/src/SpaceService"
RUN dotnet build "SpaceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpaceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpaceService.dll"]