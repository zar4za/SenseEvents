FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:6000
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SenseEvents/SenseEvents.csproj", "SenseEvents/"]
COPY ["SC.Internship.Common/SC.Internship.Common/SC.Internship.Common.csproj", "SC.Internship.Common/SC.Internship.Common/"]
RUN dotnet restore "SenseEvents/SenseEvents.csproj"
COPY . .
WORKDIR "/src/SenseEvents"
RUN dotnet build "SenseEvents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseEvents.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseEvents.dll"]