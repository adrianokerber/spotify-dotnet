# Spotify-dotnet Library a simple CRUD

This is my first project with [.NET](https://dotnet.microsoft.com/) creating a web service (ASP.NET) as a simple CRUD.

The project follows a hexagonal architecture using Domain Driven Development (DDD), then the projects are divided in:
- Domain - the layer responsible for all business rules and business models and should always be framework independent.
- Infrastructure - the layer for projects that implement framework specific code and do database communications.
- Application - the layer for the projects like WebApis, workers, etc.

We also have tests focused on BDD (Behaviour Driven Development) with SpecFlow and Unit Tests.

INFO: This repository is an increment from a project provided by [Crescer](https://crescer.cwi.com.br/).

### First steps

- Install [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/) IDE or CLI to build and run the WebAPI.
- Install [SpecFlow](https://specflow.org/)'s plugin for Visual Studio and any other requirements from its [getting started](https://specflow.org/getting-started/).
- The database (DB) is a MongoDB and it's hosted freely, but feel free to run on your own local DB by changing the keys.

Once running the service access:
- Swagger on: *http://localhost:53651/swagger/*
- Make calls to: *http://localhost:53651/api/*

### Roadmap

The following are the improvement goals from the base project.

- [x] Add MongoDB to store all data
    - [x] MusicaRepository
    - [x] AlbumRepository
- [x] Enhance RESTful API to follow REST principles (eg: when creating an entity we should return 201 instead of the simple 200)
- [x] Read Mongo connection string from configuration instead of hardcoded
- [ ] Return DTOs instead of entity classes on Controller responses
- [ ] Add new Unit Tests for invalid data such as invalid or missing **ObjectId**
- [x] Add SpecFlow BDD tests and some [examples](https://docbehat.readthedocs.io/pt/v3.1/guides/1.gherkin.html)