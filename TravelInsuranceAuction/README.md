# 🛡️ Travel Insurance Bidding Platform

An ASP.NET Core MVC web application for comparing travel insurance offers through an automated auction system. Insurance agencies compete in real time by lowering their prices, and the user selects the best policy.

---

## 🚀 Tech Stack

| Layer | Technology |
|-------|------------|
| Backend | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core 9 |
| Database | Microsoft SQL Server |
| Authentication | ASP.NET Core Identity |
| Frontend | Bootstrap 5 + Razor Views |
| Architecture | Repository Pattern + MVC |

---

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express / LocalDB)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/aleksamiljenovic/TravelInsuranceAuction.git
cd TravelInsuranceAuction
```

### 2. Configure the database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TravelInsurance;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Apply migrations (alternative)

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

The application will be available at: `https://localhost:7XXX`

---

## 👤 Test Accounts

All accounts share the same password: **`$Ifra123`**

| Role | Email | Password | Notes |
|------|-------|----------|-------|
| **Administrator** | admin@gmail.com | `$Ifra123` | Full access |
| **Traveler 1** | traveler1@gmail.com | `$Ifra123` | Verified |
| **Traveler 2** | traveler2@gmail.com | `$Ifra123` | Verified |
| **Agency 1** | agency1@gmail.com | `$Ifra123` | Pending verification |
| **Agency 2** | agency2@gmail.com | `$Ifra123` | Pending verification |
| **Agency 3** | agency3@gmail.com | `$Ifra123` | Pending verification |

> 📌 Agencies must be verified by an administrator before they can participate in auctions.

---

## 📌 Key Features

### 🔨 Auction System
- A traveler starts an auction with a duration of up to **6 hours**
- Insurance agencies automatically submit bids with decreasing prices
- Offers are displayed in **real time**, sorted by price (lowest first)
- The traveler can **manually stop** the auction by selecting a preferred offer


### 📄 Automatic Document Generation
- Instant **e-policy** issuance in PDF format after purchase

### 🔔 Notifications
- Notifications to agencies about new auctions
- Notifications to travelers about the current best offer

---

## 👥 User Roles

### Traveler (User)
- Registration and profile management
- Creating insurance requests (destination, dates, number of travelers)
- Starting and monitoring auctions
- Viewing and downloading the e-policy and invoice

### Insurance Agency
- Registration (requires administrator approval)
- Defining automatic bidding parameters (min. price, final amount, coverage conditions)
- Monitoring active auctions
- Participation history and success statistics

### Administrator
- Verifying insurance agencies
- Financial overview (platform transaction fees and revenue)
