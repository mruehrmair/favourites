#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN dotnet dev-certs https
WORKDIR /src
COPY ["Favourites.API/Favourites.API.csproj", "Favourites.API/"]
COPY ["Favourites.Data/Favourites.Data.csproj", "Favourites.Data/"]
RUN dotnet restore "Favourites.API/Favourites.API.csproj"
COPY . .
WORKDIR "/src/Favourites.API"
RUN dotnet build "Favourites.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Favourites.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Favourites.API.dll"]
ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTPS_PORT=7213