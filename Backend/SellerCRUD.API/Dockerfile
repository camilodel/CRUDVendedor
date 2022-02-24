#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SellerCRUD.API/SellerCRUD.API.csproj", "SellerCRUD.API/"]
COPY ["SellerCRUD.Services/SellerCRUD.Services.csproj", "SellerCRUD.Services/"]
COPY ["SellerCRUD.Domain/SellerCRUD.Domain.csproj", "SellerCRUD.Domain/"]
COPY ["SellerCRUD.Infraestructure/SellerCRUD.Infraestructure.csproj", "SellerCRUD.Infraestructure/"]
RUN dotnet restore "SellerCRUD.API/SellerCRUD.API.csproj"
COPY . .
WORKDIR "/src/SellerCRUD.API"
RUN dotnet build "SellerCRUD.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SellerCRUD.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SellerCRUD.API.dll"]