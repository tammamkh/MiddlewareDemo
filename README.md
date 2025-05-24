# MiddlewareDemo

This is a simple ASP.NET Core project demonstrating how to use custom middleware components.

## Features

- Custom middleware that logs request time.
- Middleware that checks for a custom header (`X-My-Key`).
- Multiple endpoints:
  - `/hello` – returns a greeting.
  - `/time` – returns current server time.

## How to Run

```bash
dotnet run
