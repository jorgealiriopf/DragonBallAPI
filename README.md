# DragonBallAPI

This project consumes the [Dragon Ball API](https://dragonball-api.com), imports selected characters into a local SQL Server database, and exposes various endpoints to query them.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/jorgealiriopf/DragonBallAPI.git
   cd DragonBallAPI
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply the migrations:
   ```bash
   dotnet ef database update --project DragonBall.Infrastructure --startup-project DragonBall.API
   ```

4. Run the project:
   ```bash
   Start-Process "http://localhost:5004/swagger"
   dotnet run --project DragonBall.API
   ```

The API will be available at: [http://localhost:5004/swagger](http://localhost:5004/swagger)

## Authentication

All endpoints require **Basic Authentication**.

- **Username:** `admin`
- **Password:** `1234`

## 🔎 Endpoints

- `GET /api/characters` – Get all characters
- `GET /api/characters/{id}` – Get character by ID
- `GET /api/transformations` – Get all transformations
- `POST /api/characters/sync` – Import characters from external API
- `GET /api/characters/search/by-name?name=Goku` – Search characters by name
- `GET /api/characters/search/by-affiliation?affiliation=Z Fighter` – Search characters by affiliation
