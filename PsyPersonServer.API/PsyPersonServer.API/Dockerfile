#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PsyPersonServer.API/PsyPersonServer.API.csproj", "PsyPersonServer.API/"]
RUN dotnet restore "PsyPersonServer.API/PsyPersonServer.API.csproj"
COPY . .
WORKDIR "/src/PsyPersonServer.API"
RUN dotnet build "PsyPersonServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PsyPersonServer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsyPersonServer.API.dll"]