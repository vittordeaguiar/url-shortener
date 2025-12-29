> **[🇧🇷 Leia em Português](README.md)**

# URL Shortener

This is a project I built to study and put into practice some stuff from the .NET ecosystem.

## What's inside?

I used this project to play around with some current technologies and reinforce some architectural concepts:

*   **.NET 10**: The new .NET version.
*   **Minimal APIs**: No Controllers. I used endpoints directly on the builder, keeping the code cleaner and faster.
*   **Clean Architecture (Simplified)**: I separated the project into logical directories to keep things organized:
    *   `Domain`: Entities and interfaces.
    *   `Application`: Shortening logic.
    *   `Infrastructure`: DB connection.
    *   `Endpoints`: Where API routes are defined.
*   **EF Core + PostgreSQL**
*   **Docker & Docker Compose**

## How to run

The easiest way to run this is using Docker, as it spins up both the database and the application ready to go.

1.  Make sure you have **Docker** and **Docker Compose** installed.
2.  Clone the repository and enter the root folder.
3.  Run the command:

```bash
docker-compose up --build
```

This will start the API and Postgres. The system is configured to run database migrations automatically when the application starts, so you don't need to worry about creating tables manually.

If you want to run it locally without Docker, you'll need a running Postgres instance and will have to adjust the `ConnectionStrings` in `appsettings.Development.json` before running `dotnet run`.

### Shorten a URL

Send a POST request to `/shorten` with a JSON body:

```json
{
  "url": "https://www.google.com/search?q=dotnet+10"
}
```

The response will be something like:

```json
{
  "code": "Xy7z9A",
  "shortUrl": "http://localhost:5000/Xy7z9A"
}
```

### Access the URL

Just paste the `shortUrl` into your browser or make a GET request to `/{code}` (e.g., `http://localhost:5000/Xy7z9A`). The API will redirect you to the original link.
