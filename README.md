# Minecraft Server List

Веб застосунок для пошуку та публікації Minecraft серверів.  
Користувачі можуть переглядати сервери, додавати власні, залишати оцінки та відгуки.

---

## 🔧 Технології

- **Backend:** ASP.NET Core (C#)
- **Database:** PostgreSQL
- **Frontend:** Vue.js
- **Архітектура:** Layered + Clean Architecture
- **Патерни:** Strategy, Repository, Factory Method, Unit of Work

---

## 🚀 Основний функціонал

- 🔍 Пошук серверів з фільтрами та сортуванням
- ➕ Додавання власного сервера
- ⭐ Система рейтингу (1–5 зірок)
- 💬 Відгуки користувачів
- 👤 Реєстрація та авторизація (JWT)
- 🛠 Ролі: User, Moderator, Admin
- 🌐 Перевірка статусу сервера (Online/Offline, players)

---

## 🏗 Архітектура

Проєкт побудований за принципами Clean Architecture:

- **API** — контролери та конфігурація
- **Application** — use cases, бізнес логіка
- **Domain** — сутності
- **Infrastructure** — база даних, репозиторії

---

## ⚙️ Запуск проєкту

### Backend

```
cd ServerList.Api
dotnet restore
dotnet run
```

### Database

- PostgreSQL
- налаштувати connection string в `appsettings.json`
- виконати міграції:

```
dotnet ef database update
```

### Frontend

```
cd client
npm install
npm run dev
```

---

## 📡 API

Для тестування API використовується **Scalar (OpenAPI UI)**

Доступно після запуску бекенду:

```
https://localhost:xxxx/scalar
```

---

## 🧠 Архітектурні рішення

- **Strategy** — фільтрація та сортування серверів
- **Repository** — робота з базою даних
- **Factory Method** — створення перевіряльника серверів
- **Unit of Work** — централізоване збереження змін

---

## 📌 Статус проєкту

Проєкт знаходиться у стадії розробки.  
Основна бізнес логіка реалізована, frontend частково готовий.

---

## 📄 Автор

Курсовий проєкт  
Minecraft Server List
