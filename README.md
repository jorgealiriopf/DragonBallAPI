# DragonBallAPI

This project consumes the [Dragon Ball API](https://dragonball-api.com), imports selected characters into a local SQL Server database, and exposes various endpoints to query them.

## Features

- Imports **Saiyan** and **Z Fighter** characters from the external Dragon Ball API.
- Stores characters and their transformations locally.
- Basic Authentication for protected endpoints.
- Query support by name or affiliation.

## Installation

1. **Clone the repository**
   ```bash
   https://github.com/jorgealiriopf/DragonBallAPI.git
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Apply the database migrations**
   ```bash
   dotnet ef database update --project DragonBall.Infrastructure --startup-project DragonBall.API
   ```

4. **Run the project**
   ```bash
   Start-Process "https://localhost:7261/swagger"
   dotnet run --project DragonBall.API
   ```

   > The Swagger UI will open automatically at [https://localhost:7261/swagger](https://localhost:7261/swagger)

5. **Import data (Important!)**

   After launching the API, **you must call the following endpoint** to import the character data:
   ```
   POST /api/characters/sync
   ```

   Without this, the database will remain empty.

## Authentication

All endpoints require Basic Authentication:

- **Username**: `admin`
- **Password**: `1234`

Click "Authorize" in Swagger and enter the credentials.

## Endpoints

- `POST /api/characters/sync` – Import characters from external API
- `GET /api/characters` – List all characters
- `GET /api/characters/{id}` – Get character by ID
- `GET /api/transformations` – List all transformations
- `GET /api/characters/search/by-name?name=Goku` – Search by character name
- `GET /api/characters/search/by-affiliation?affiliation=Z Fighter` – Search by affiliation
