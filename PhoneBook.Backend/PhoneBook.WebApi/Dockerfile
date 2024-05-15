FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5757
EXPOSE 5858

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PhoneBook.WebApi/PhoneBook.WebApi.csproj", "PhoneBook.WebApi/"]
RUN dotnet restore "PhoneBook.WebApi/PhoneBook.WebApi.csproj"
COPY . .
RUN dotnet tool install --global dotnet-ef
RUN /root/.dotnet/tools/dotnet-ef database update

WORKDIR "/src/PhoneBook.WebApi"
RUN dotnet build "PhoneBook.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PhoneBook.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.WebApi.dll"]
