# ShopNow - Full-Stack E-Commerce Application

A production-ready full-stack e-commerce application built with **Next.js 14** (frontend) and **ASP.NET Core 8** (backend) following Clean Architecture.

---

## 📁 Project Structure

```
e-commerceApp/
├── backend/
│   ├── ECommerceApp.sln
│   ├── ECommerceApp.Domain/           ← Entities, Interfaces, Enums
│   │   ├── Entities/
│   │   ├── Interfaces/
│   │   └── Enums/
│   ├── ECommerceApp.Application/      ← Business Logic
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   ├── Mappings/
│   │   ├── Validators/
│   │   └── Common/
│   ├── ECommerceApp.Infrastructure/   ← EF Core, Repositories
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   └── Migrations/
│   │   ├── Repositories/
│   │   └── Services/
│   └── ECommerceApp.API/              ← Controllers, Middleware
│       ├── Controllers/
│       ├── Middleware/
│       └── Extensions/
└── frontend/
    ├── app/
    │   ├── login/
    │   ├── register/
    │   ├── products/
    │   │   └── [id]/
    │   ├── cart/
    │   ├── checkout/
    │   ├── orders/
    │   └── admin/
    │       ├── products/
    │       └── categories/
    ├── components/
    ├── store/       ← Zustand state management
    ├── hooks/
    ├── lib/         ← Axios configuration
    └── types/
```

---

## 🚀 Getting Started (Local Development)

### Prerequisites
- .NET 8 SDK
- Node.js 20+
- MySQL 8.0
- npm

---

### Step 1: Setup MySQL

Create the database:
```sql
CREATE DATABASE ecommerce_db;
CREATE USER 'ecommerce_user'@'localhost' IDENTIFIED BY 'ecommerce_pass';
GRANT ALL PRIVILEGES ON ecommerce_db.* TO 'ecommerce_user'@'localhost';
FLUSH PRIVILEGES;
```

---

### Step 2: Configure Backend

Edit `backend/ECommerceApp.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=ecommerce_db;User=root;Password=YOUR_PASSWORD;"
  },
  "JwtSettings": {
    "Secret": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "ECommerceApp",
    "Audience": "ECommerceApp"
  }
}
```

---

### Step 3: Run Backend

```bash
cd backend/ECommerceApp.API
dotnet run
```

The API will be available at `http://localhost:5000`
Swagger UI: `http://localhost:5000/swagger`

The database will auto-migrate and seed on first run with:
- 4 categories (Electronics, Clothing, Books, Home & Kitchen)
- 6 products
- 1 admin user: `admin@ecommerce.com` / `Admin@123`

---

### Step 4: Run Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend: `http://localhost:3000`

---

## 🐳 Docker Deployment

```bash
# From project root
docker-compose up -d
```

Services:
- MySQL: `localhost:3306`
- Backend API: `http://localhost:5000`
- Frontend: `http://localhost:3000`

---

## 🗄️ Database Schema

```sql
Users          (Id, Name, Email, PasswordHash, Role)
Categories     (Id, Name)
Products       (Id, Name, Description, Price, Stock, ImageUrl, CategoryId)
Carts          (Id, UserId)
CartItems      (Id, CartId, ProductId, Quantity)
Orders         (Id, UserId, TotalAmount, Status, CreatedAt)
OrderItems     (Id, OrderId, ProductId, Quantity, Price)
```

---

## 🔌 API Endpoints

### Auth
| Method | Endpoint | Auth |
|--------|----------|------|
| POST | /api/auth/register | Public |
| POST | /api/auth/login | Public |

### Products
| Method | Endpoint | Auth |
|--------|----------|------|
| GET | /api/products?page=1&pageSize=10&search=&categoryId= | Public |
| GET | /api/products/{id} | Public |
| POST | /api/products | Admin |
| PUT | /api/products/{id} | Admin |
| DELETE | /api/products/{id} | Admin |

### Categories
| Method | Endpoint | Auth |
|--------|----------|------|
| GET | /api/categories | Public |
| POST | /api/categories | Admin |
| PUT | /api/categories/{id} | Admin |
| DELETE | /api/categories/{id} | Admin |

### Cart
| Method | Endpoint | Auth |
|--------|----------|------|
| GET | /api/cart | User |
| POST | /api/cart/items | User |
| PUT | /api/cart/items/{id} | User |
| DELETE | /api/cart/items/{id} | User |
| DELETE | /api/cart | User |

### Orders
| Method | Endpoint | Auth |
|--------|----------|------|
| POST | /api/orders/checkout | User |
| GET | /api/orders | User |
| GET | /api/orders/{id} | User |

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────┐
│                   API Layer                  │
│  Controllers → Middleware → Extensions       │
└──────────────────┬──────────────────────────┘
                   │ depends on
┌──────────────────▼──────────────────────────┐
│              Application Layer              │
│  Services → DTOs → AutoMapper → Validators  │
└──────────────────┬──────────────────────────┘
                   │ depends on
┌──────────────────▼──────────────────────────┐
│               Domain Layer                  │
│  Entities → Interfaces → Enums              │
└─────────────────────────────────────────────┘
                   ▲ implements
┌──────────────────┴──────────────────────────┐
│            Infrastructure Layer             │
│  EF Core → Repositories → JWT Service       │
└─────────────────────────────────────────────┘
```

---

## 🔐 Authentication Flow

1. User registers/logs in → receives JWT token
2. Frontend stores token in browser cookie via `js-cookie`
3. Axios interceptor automatically attaches `Authorization: Bearer <token>` header
4. Backend validates JWT on protected endpoints
5. Role-based authorization: `[Authorize(Roles = "Admin")]`

---

## 📦 Key Technologies

### Backend
| Package | Version | Purpose |
|---------|---------|---------|
| ASP.NET Core | 8.0 | Web framework |
| Entity Framework Core | 8.0 | ORM |
| Pomelo.EntityFrameworkCore.MySql | 8.0 | MySQL provider |
| AutoMapper | 13.0.1 | Object mapping |
| FluentValidation | 11.9.0 | Input validation |
| BCrypt.Net-Next | 4.0.3 | Password hashing |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger/OpenAPI |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0 | JWT auth |

### Frontend
| Package | Purpose |
|---------|---------|
| Next.js 14 | React framework with App Router |
| TypeScript | Type safety |
| Tailwind CSS | Styling |
| Axios | HTTP client |
| Zustand | State management |
| js-cookie | Cookie management |
| react-hot-toast | Notifications |
| lucide-react | Icons |

---

## 🚀 Production Deployment

### Environment Variables

**Backend (Production):**
```
ConnectionStrings__DefaultConnection=<mysql-connection-string>
JwtSettings__Secret=<strong-random-secret-min-32-chars>
JwtSettings__Issuer=ECommerceApp
JwtSettings__Audience=ECommerceApp
AllowedOrigins=https://your-frontend-domain.com
ASPNETCORE_ENVIRONMENT=Production
```

**Frontend (Production):**
```
NEXT_PUBLIC_API_URL=https://your-api-domain.com/api
```

### Deployment Steps

1. **Build backend**: `dotnet publish -c Release`
2. **Run migrations**: `dotnet ef database update`
3. **Build frontend**: `npm run build`
4. **Deploy with Docker**: `docker-compose -f docker-compose.prod.yml up -d`

---

## 🔑 Default Credentials

| Role | Email | Password |
|------|-------|---------|
| Admin | admin@ecommerce.com | Admin@123 |

---

## 📋 Features Checklist

- [x] User Registration & Login
- [x] JWT Authentication
- [x] Role-based Authorization (Admin/Customer)
- [x] Product CRUD (Admin)
- [x] Category CRUD (Admin)
- [x] Product listing with pagination & search
- [x] Product detail page
- [x] Add to Cart
- [x] Update/Remove cart items
- [x] Checkout → Create Order
- [x] Order history
- [x] Admin dashboard
- [x] Global exception handling
- [x] FluentValidation
- [x] AutoMapper profiles
- [x] Swagger UI
- [x] CORS configured
- [x] Seed data
- [x] Docker support
- [x] Environment variables
