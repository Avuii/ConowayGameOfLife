# 🧬 Conway Game of Life — Blazor WebAssembly + ASP.NET Core API

**Interactive implementation of Conway’s Game of Life**, built with **Blazor WebAssembly** (frontend), **ASP.NET Core Web API** (backend), and **SQL Server** (persistent storage).

The application allows users to:
- Create and customize boards  
- Toggle cells manually  
- Step through generations  
- Run automated simulations  
- Save, load, and delete boards stored in a database  

---

## 🎮 Project Overview

This project recreates the famous **Conway's Game of Life**, a classic cellular automaton that demonstrates how complex behavior emerges from simple rules.

It is built using a three-project architecture:

- **Blazor WebAssembly** — the interactive UI  
- **ASP.NET Core API** — manages board storage  
- **Shared library** — contains DTOs shared between client & server  

This separation follows clean architectural patterns and enables the client to communicate with the backend through REST.

---

## ⚙️ Features

### 🟩 Simulation Controls
- Next generation  
- Clear board  
- Adjustable width & height  
- Create new board  
- Auto-play for multiple generations  

### 💾 Persistent Storage (API + SQL Server)
- Save boards with custom names  
- List previously saved boards  
- Load a selected board  
- Delete saved entries  

### 🎨 UI Highlights
- Responsive sidebar menu  
- Bootstrap 5 modern styling  
- Inline success messages  
- Interactive table with saved boards  

---

## 🛠️ Technology Stack

| Layer     | Technology |
|-----------|------------|
| Frontend  | Blazor WebAssembly (.NET 8) |
| Backend   | ASP.NET Core 8 Web API |
| Database  | SQL Server |
| ORM       | Entity Framework Core |
| UI        | Bootstrap 5 |
| Shared    | .NET Class Library (DTOs) |

---

## 📡 API Endpoints

### `GET /api/boards`
Returns a list of all saved boards.

### `GET /api/boards/{id}`
Returns the full board including its cell matrix.

### `POST /api/boards`
Saves a board to the database.

### `DELETE /api/boards/{id}`
Deletes a board from the database.

Boards are stored in compact string format: 1,0,1;0,1,0;1,1,1  

---

## 📂 Repository Structure
ConowayGameOfLife/  
├── ConowayGameOfLife/ # Blazor WebAssembly client  
│ ├── Pages/  
│ ├── Layout/  
│ ├── wwwroot/  
│ ├── Program.cs  
│ └── GameOfLifeService.cs  
│  
├── ConowayGameOfLife.Api/ # ASP.NET Core Web API  
│ ├── Controllers/  
│ │ └── GameBoardController.cs  
│ ├── Data/  
│ │ └── AppDbContext.cs  
│ ├── Models/  
│ │ └── GameBoardEntity.cs  
│ └── Program.cs  
│  
├── ConowayGameOfLife.Shared/ # Shared DTOs  
│ ├── BoardDTo.cs  
│ └── BoardSummaryDto.cs  
│  
└── README.md  

---

## 🧠 Game Logic (Summary)

The Game of Life evolves based on simple rules applied to each cell:

1. **Underpopulation:**    
   A live cell with fewer than 2 neighbors dies.  
2. **Survival:**    
   A live cell with 2–3 neighbors lives on.  
3. **Overpopulation:**    
   A live cell with more than 3 neighbors dies.  
4. **Reproduction:**  
   A dead cell with exactly 3 neighbors becomes alive.  

---

## 🚀 Running the Project Locally
(Full Guide)  
This project consists of two applications that must run at the same time:  
* ConowayGameOfLife.Api — backend (ASP.NET Core Web API + SQL Server)  
* ConowayGameOfLife — frontend (Blazor WebAssembly)  

Follow the steps below to prepare your environment.  

#### 🧱 1. Prerequisites  
Before running the project, install:  
Install the following:  
✔️ .NET SDK 8  
https://dotnet.microsoft.com/en-us/download/dotnet/8.0  

✔️ SQL Server    
(Express / Developer Edition / LocalDB)  

✔️ SQL Server Management Studio (optional)  
https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

✔️ Entity Framework Core Tools  
  1. Run the following command in PowerShell or Command Prompt:  
  ```dotnet tool install --global dotnet-ef```  
  You can verify installation with:   
  ```dotnet ef```  
  2. Install EF Core packages for the API project     
  NuGet packages required by the backend:    

| Package                                   | Description           |
|-------------------------------------------|-----------------------|
| Microsoft.EntityFrameworkCore             | Core EF functionality |
| Microsoft.EntityFrameworkCore.SqlServer   | SQL Server provider   |
| Microsoft.EntityFrameworkCore.Tools       | Migration support     |
  

---

#### 📥 2. Clone the Repository
```git
git clone https://github.com/YourUserName/ConowayGameOfLife.git
cd ConowayGameOfLife
```
Open solution in Visual Studio 2022.

#### 🗄️ 3. Configure Database Connection
Open:  ```ConowayGameOfLife.Api/appsettings.json```  
```csharp
"ConnectionStrings": {
  "AppDbContext": "Server=YOUR_SERVER_NAME;Database=GameOfLifeDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace YOUR_SERVER_NAME with your local SQL Server instance.

#### 🛠️ 4. Apply Database Migrations
In Visual Studio:  
Open Package Manager Console:  

Tools → NuGet Package Manager → Package Manager Console  

Run: ``` Update-Database```  

This will automatically create:  
* The database GameOfLifeDB  
* The table GameBoards  

#### 🧩 5. Start the API  
In Visual Studio:  
- Right-click ConowayGameOfLife.Api  
- Select Set as Startup Project
- Click Run ▶️

API should start on: ``` https://localhost:7183```  
Check it in the browser: ```https://localhost:7183/swagger```   

You should see the Swagger UI.   

#### 🌐 6. Configure the Blazor Client  
Open:  ```ConowayGameOfLife/Program.cs```    

Make sure the API URL is correct:    
```csharp
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7183/") });
```  

This means Blazor client sends all requests to your running API.  

#### 🖥️ 7. Start the Blazor WebAssembly App

Now set frontend as startup:    
1. Right-click ConowayGameOfLife    
2. Choose Set as Startup Project   
3. Click Run ▶️
  
Blazor app will start at:  ```http://localhost:5052```    

#### 🔄 8. Run Both Projects Together  
You can also configure multiple startup projects:  
Visual Studio → Solution properties → Startup Project   
  
| Project                  | Action |  
| ------------------------ | ------ |  
| ConowayGameOfLife.Api    | Start  |  
| ConowayGameOfLife        | Start  |  
| ConowayGameOfLife.Shared | None   |  

Click Apply → OK  
Now both API + client will launch with a single Run click.  

#### 🧪 9. Confirm Everything Works  

Open Blazor app → click Table Data ▼  
You should NOT see errors.    
You should see an empty list or saved boards.  

Try:    
* Create a board
* Save it
* Open SQL Server → see the DB entry
* Load/Delete
If it works — the environment is fully configured.  


---
🧑‍💻 Author

Created by Avuii  


