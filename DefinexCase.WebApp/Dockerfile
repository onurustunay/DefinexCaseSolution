#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DefinexCase.WebApp/DefinexCase.WebApp.csproj", "DefinexCase.WebApp/"]
COPY ["DefinexCase.Common/DefinexCase.Common.csproj", "DefinexCase.Common/"]
COPY ["DefinexCase.Business.Services/DefinexCase.Business.Services.csproj", "DefinexCase.Business.Services/"]
COPY ["DefinexCase.Data.Repository/DefinexCase.Data.Repository.csproj", "DefinexCase.Data.Repository/"]
COPY ["DefinexCase.Data.Entity/DefinexCase.Data.Entity.csproj", "DefinexCase.Data.Entity/"]
COPY ["DefinexCase.Business.Models/DefinexCase.Business.Models.csproj", "DefinexCase.Business.Models/"]
RUN dotnet restore "DefinexCase.WebApp/DefinexCase.WebApp.csproj"
COPY . .
WORKDIR "/src/DefinexCase.WebApp"
RUN dotnet build "DefinexCase.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DefinexCase.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DefinexCase.WebApp.dll"]