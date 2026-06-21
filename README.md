# Warehouse API

Тестовое задание – REST API сервис учёта товаров на складе.

## Как запустить проект

### Вариант 1: Запуск через Docker Compose

Для сборки и запуска приложения вместе с базой данных PostgreSQL выполните команду в корне проекта:

```bash
docker compose up -d --build
```

После запуска:
* Swagger UI доступен по адресу: http://localhost:8081/swagger/index.html.
* База данных создастся, применит миграции и заполнится стартовыми данными (3 категории и 6 товаров).

Для остановки контейнеров:
```bash
docker compose down
```

### Вариант 2: Локальный запуск (без Docker)

Для запуска требуется установленный .NET 10 SDK и запущенный PostgreSQL.

1. Укажите строку подключения к PostgreSQL в файле `src/WarehouseApi.Api/appsettings.Development.json` в секции `ConnectionStrings:DefaultConnection`.
2. Запустите проект с помощью IDE или из корня репозитория:
```bash
dotnet run --project src/WarehouseApi.Api/WarehouseApi.Api.csproj
```

После запуска:
* **Swagger UI** доступен по адресу: http://localhost:5072/swagger/index.html.
* База данных создастся, применит миграции и заполнится стартовыми данными (3 категории и 6 товаров).

---

## Переменные окружения

При запуске через Docker Compose используются следующие переменные окружения:

* **Для базы данных:**
  * `POSTGRES_USER` – имя пользователя БД (`postgres`)
  * `POSTGRES_PASSWORD` – пароль пользователя БД (`postgres`)
  * `POSTGRES_DB` – название базы данных (`warehouse`)
* **Для backend-сервиса:**
  * `ASPNETCORE_ENVIRONMENT` – окружение запуска приложения (`Production`)
  * `ConnectionStrings__DefaultConnection` – строка подключения к PostgreSQL

---

## Тестирование

Для проверки бизнес-правил переходов статусов реализованы Unit-тесты.

Для запуска тестов выполните команду в корне репозитория:

```bash
dotnet test
```
