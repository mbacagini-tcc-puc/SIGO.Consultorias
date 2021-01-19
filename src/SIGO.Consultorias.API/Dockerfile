#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SIGO.Consultorias.API/SIGO.Consultorias.API.csproj", "src/SIGO.Consultorias.API/"]
RUN dotnet restore "src/SIGO.Consultorias.API/SIGO.Consultorias.API.csproj"
COPY . .
WORKDIR "/src/src/SIGO.Consultorias.API"
RUN dotnet build "SIGO.Consultorias.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SIGO.Consultorias.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SIGO.Consultorias.API.dll"]