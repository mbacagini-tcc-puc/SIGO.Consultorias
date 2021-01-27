FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish src/SIGO.Consultorias.API -c Release -o publish


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "SIGO.Consultorias.API.dll"]