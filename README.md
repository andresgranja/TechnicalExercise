
# Technical Exercise API

This project is part of a technical exercise and includes a .NET Core API that interacts with a SQLite database. It's structured using Clean Architecture and includes Docker support for easy setup and deployment.

## Prerequisites

Before you begin, ensure you have the following installed:
- .NET SDK (compatible with .NET 8.0)
- Docker Desktop (for running the application in a container)
- Visual Studio (or another IDE with support for .NET development)

## Project Structure

- `TechnicalExercise.API`: The main web API project.
- `TechnicalExercise.Application`: Contains the business logic.
- `TechnicalExercise.Core`: Includes domain models and interfaces.
- `TechnicalExercise.Infrastructure`: Handles data persistence and other infrastructure tasks.

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/andresgranja/TechnicalExercise.git
   cd TechnicalExercise
   ```

2. **Build the Solution**
   Open the solution in Visual Studio and build it to restore the necessary packages.

3. **Run Locally**
   You can run the application locally by setting `TechnicalExercise.API` as the startup project in Visual Studio and pressing F5.

4. **Using Docker**
   To build and run the application using Docker, navigate to the solution root and run:
   ```bash
   docker build -t libraryapi -f .\TechnicalExercise.API\Dockerfile .
   docker run -p 8080:80 libraryapi
   ```

5. **Access the API**
   Once the application is running, you can access the API at:
   ```
   http://localhost:8080
   ```

## Docker Compose

If you prefer to use Docker Compose, use the following commands:

```bash
docker-compose up -d  # Runs in detached mode
```

To stop the containers:

```bash
docker-compose down
```