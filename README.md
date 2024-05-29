# Phonebook


- [Стэк](#стэк)
- [Эндпоинты API](#эндпоинты-api)
- [Использование приложения](#использование-приложения)

---

## Стэк
### Серверная часть
- **Asp Net Core 8**
- Логирование - **Serilog**
- **EntityFramework Core 8** (ORM для взаимодействия с БД)
- Для автодокументирования API - используется **Swagger**
- Аутентификация и Авторизация - **Identity Core**
- База данных - **PostgreSQL**

### Клиентская часть
- **React v. 18.2.0**
- Для некоторых компонентов - **Ant Design**
- Инструмент сборки - **Vite**

---

## Эндпоинты API

### `api/phonebook`

### Все записи [GET]

*Авторизация не требуется**

*Без параметров**
> api/phonebook/

Возвращает список всех записей в телефонном справочнике.

### Запись по идентификатору [GET]

*Авторизация не требуется**

*Id (Integer) - обязателен**
> api/phonebook/{id}

Возвращает единичную запись из базы данных.

### Создание записи [POST]

*Требутеся авторизация**

*Без параметров**
> api/phonebook/

POST-запрос на создание записи в базе данных.

JSON для отправки (контент в body):
```json
{
    "id": 0,
    "lastname": "string",
    "firstname": "string",
    "middlename": "string",
    "phonenumbers": [
        {
            "type": "string",
            "number": "string"
        }
    ],
    "email": "user@example.com",
    "address": "string",
    "post": "string",
    "organization": "string",
    "subdivision": "string"
}
```

### Обновление записи [PUT]

*Требутеся авторизация**

*Id (Integer) - обязателен**
> api/phonebook/{id}

PUT-запрос на обновление единичной записи

JSON для отправки (контент в body):
```json
{
    "lastname": "string",
    "firstname": "string",
    "middlename": "string",
    "phonenumbers": [
        {
            "type": "string",
            "number": "string"
        }
    ],
    "email": "user@example.com",
    "address": "string",
    "post": "string",
    "organization": "string",
    "subdivision": "string"
}
```

### Удаление записи [DELETE]

*Требутеся авторизация**

*Id (Integer) - обязателен**
> api/phonebook/{id}

DELETE-запрос единичной записи из базы данных

### `api/sync-json`

*Требуется авторизация**

*Без параметров**

*Принимает данные с html-форм**

Принимает один или более файло в формате `.json`. Структура JSON в файлах может быть такой:
```json
{
    "id": 0,
    "lastname": "string",
    "firstname": "string",
    "middlename": "string",
    "phonenumbers": [
      {
        "type": "string",
        "number": "string"
      }
    ],
    "email": "user@example.com",
    "address": "string",
    "post": "string",
    "organization": "string",
    "subdivision": "string"
}
```

В клиентском приложении input должен иметь название `files`.

### `api/account/`

### Логин [POST]

*Авторизация не требуется**

*Без параметров**

> api/account/login

Авторизация в API. Пример JSON для отправки по эдпоинту:
```json
{
  "username": "string",
  "password": "string"
}
```

### Регистрация [POST]

*Авторизация не требуется**

*Без параметров**

> api/account/register

Аутентификация в API. Пример JSON для отправки по эдпоинту:
```json
{
  "username": "string",
  "role": "string",
  "password": "string",
  "confirmPassword": "string"
}
```
Свойство `role` принимает значения `"Admin"` или `"User"`.

---

## Использование приложения

### Docker compose
`docker-compose up` - запуск (Остановить `CTRL+C`)

`docker-compose up -d` - запуск в режиме демона

`docker-compose stop` - для остановки

`docker-compose down` - остановка с удалением

### Логин и пароль

Логин для входа: `test`

Пароль для входа: `Test@1234567`

### Маршрутизация

- `localhost:80` - открывается (с помощь NGINX) клиентское приложение
- `localhost/backend/` - все проксированные запросы к API (для клиентских приложений)

### Адреса по умолчанию

- `http://localhost:5888` - Серверное приложение (API)
- `http://localhost:5123` - Клиентское приложение (React)
- `http://localhost:5432` - База данных (PostgeSQL)
  - Пароль по умолчанию: `123`
  - Пользователь по умолчанию: `postgres`
  - База данных по умолчанию: `phonebookdb`
- NGINX по умолчанию слушает **80** порт

