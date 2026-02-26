# ShopNow — Full-Stack E-Commerce App

A full-stack e-commerce application built with **Next.js 14** (frontend) and **ASP.NET Core 8** (backend).

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Next.js 14, TypeScript, Tailwind CSS, Zustand |
| Backend | ASP.NET Core 8, Entity Framework Core, Clean Architecture |
| Database | MySQL 8 |
| Auth | JWT Tokens, Role-based (Admin / Customer) |

---

## Prerequisites

- .NET 8 SDK
- Node.js 20+
- MySQL 8

---

## Run Locally

### 1. Configure the database

Update `backend/ECommerceApp.API/appsettings.json`:
```json
"DefaultConnection": "Server=localhost;Port=3306;Database=e-commerce;User=root;Password=YOUR_PASSWORD;"
```

### 2. Run the backend
```bash
cd backend/ECommerceApp.API
dotnet run
```
API → `http://localhost:5110`  
Swagger → `http://localhost:5110/swagger`

### 3. Run the frontend
```bash
cd frontend
npm install
npm run dev
```
App → `http://localhost:3000`

---

## Default Admin Account

| Email | Password |
|---|---|
| admin@ecommerce.com | Admin@123 |

---

## Features

- User registration & login with JWT
- Product listing with search & pagination
- Shopping cart & checkout
- Order history
- Admin panel (manage products & categories)
- Seed data (4 categories, 6 products, 1 admin)

---

## Docker

```bash
docker-compose up -d
```
