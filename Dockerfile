#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CursoAngular.API/CursoAngular.API.csproj", "CursoAngular.API/"]
COPY ["CursoAngular.DAL/CursoAngular.DAL.csproj", "CursoAngular.DAL/"]
COPY ["CursoAngular.BOL/CursoAngular.BOL.csproj", "CursoAngular.BOL/"]
COPY ["CursoAngular.Repository/CursoAngular.Repository.csproj", "CursoAngular.Repository/"]
COPY ["CursoAngular.UOW/CursoAngular.UOW.csproj", "CursoAngular.UOW/"]
RUN dotnet restore "CursoAngular.API/CursoAngular.API.csproj"
COPY . .
WORKDIR "/src/CursoAngular.API"
RUN dotnet build "CursoAngular.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CursoAngular.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CursoAngular.API.dll"]