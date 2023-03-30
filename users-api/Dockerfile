#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UsersService.Api/UsersService.Api.csproj", "UsersService.Api/"]
COPY ["src/UsersService.Business/UsersService.Business.csproj", "UsersService.Business/"]
COPY ["src/UsersService.Data/UsersService.Data.csproj", "UsersService.Data/"]
RUN dotnet restore "UsersService.Api/UsersService.Api.csproj"
COPY . .
WORKDIR "src/UsersService.Api"
RUN dotnet build "UsersService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersService.Api.dll"]