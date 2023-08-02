# Используйте образ ASP.NET Core SDK для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Устанавливаем рабочую директорию
WORKDIR /app

# # Копируем файлы проекта и восстанавливаем зависимости
# COPY ["AStar.Organization.Infrastructure.API/", "AStar.Organization.Infrastructure.API/"]
# COPY ["AStar.Organization.Infrastructure.DAL/", "AStar.Organization.Infrastructure.DAL/"]
# COPY ["AStar.Organization.Infrastructure.BLL/", "AStar.Organization.Infrastructure.BLL/"]
# COPY ["AStar.Organization.Core.DomainServices/", "AStar.Organization.Core.DomainServices/"]
# COPY ["AStar.Organization.Core.Domain/", "AStar.Organization.Core.Domain/"]
# COPY ["AStar.Organization.Core.Application/", "AStar.Organization.Core.Application/"]

# Копируем все остальные файлы и выполняем сборку проекта
COPY . .

# Восстанавливаем зависимости для всех проектов
RUN dotnet restore ./AStar.Organization.Infrastructure.API/AStar.Organization.Infrastructure.API.csproj

# Здесь выполняем сборку API проекта, но также убедитесь, что у вас есть команды для сборки других проектов, если это необходимо.
RUN dotnet publish -c Release -o out ./AStar.Organization.Infrastructure.API/AStar.Organization.Infrastructure.API.csproj

# Используем образ ASP.NET Core Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем выходные файлы из сборки API проекта
COPY --from=build-env /app/AStar.Organization.Infrastructure.API/out .

# Указываем, на каком порту будет работать приложение
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "AStar.Organization.Infrastructure.API.dll"]