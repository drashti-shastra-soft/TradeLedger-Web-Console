# 📊 TradeLedger – Trading Journal Web Console

**TradeLedger Web Console** is a modern and responsive web-based administration platform designed to manage and analyze trading activities.

It provides a centralized interface for tracking trades, monitoring profit and loss (P&L), managing users, reviewing trading performance, and understanding trading patterns through analytics.

## 🚀 Features

- **Dashboard:** View trading statistics and important performance information.
- **Trade Management:** Record, view, and manage trading activities.
- **Trading Journal:** Maintain organized trading records and history.
- **CSV Import:** Import trading data from supported files.
- **P&L Analysis:** Track realized and unrealized profit and loss.
- **Analytics:** Analyze trading performance and identify patterns.
- **Watchlist Management:** Monitor trading opportunities and setups.
- **Risk Management:** Review risk and reward metrics.
- **AI Trade Review:** Analyze trading activities and identify recurring mistakes.
- **Market Insights:** Access market-related information and insights.
- **User Management:** Manage platform users and access.
- **Reports:** Review trading performance and historical data.
- **Responsive Design:** Access the console across different screen sizes.

## 🛠️ Technology Stack

- ASP.NET / .NET (as configured in the solution)
- C#
- HTML5
- CSS3
- JavaScript
- RESTful API Integration
- SQL Server (if configured as the database provider)

## 🏗️ Project Architecture

The application is organized into separate layers to improve maintainability, scalability, and code reusability.

```text
TradeLedger-Web-Console/
│
├── TradeLedgerWebConsole.Domain/
│
├── TradeLedgerWebConsole.Infrastructure/
│
├── TradeLedgerWebConsole.Service/
│
├── TradeLedgerWebConsole/
│
└── TradeLedgerWebConsole.sln
```

### Layer Responsibilities

- **Domain:** Contains application models and domain-related definitions.
- **Infrastructure:** Handles infrastructure and data access-related functionality.
- **Service:** Contains business logic and service-layer operations.
- **WebConsole:** Handles the web interface, controllers, views, and user interactions.

## ⚙️ Getting Started

### Prerequisites

- Visual Studio
- Compatible .NET SDK / Targeting Pack
- SQL Server (if required)
- NuGet Package Manager
- TradeLedger Web API

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/drashti-shastra-soft/TradeLedger-Web-Console.git
```

**2. Navigate to the project directory**

```bash
cd TradeLedger-Web-Console
```

**3. Open the solution**

Open the solution file in Visual Studio:

```text
TradeLedgerWebConsole.sln
```

**4. Restore NuGet Packages**

Restore all required NuGet packages.

**5. Configure API Settings**

Configure the TradeLedger Web API base URL and other required application settings.

**6. Build and Run**

Build the solution and run the Web Console using Visual Studio.

## 🔐 Security

- Secure authentication.
- Role-based access control (if enabled).
- Protected application features.
- Secure API communication.
- Configuration-based application settings.

## 🎯 Project Objective

The main objective of TradeLedger is to provide traders with a centralized platform to record trading activities, monitor financial performance, identify trading patterns, and make better-informed decisions through structured data and analytics.

---

⭐ If you find this project useful, don't forget to star the repository!
