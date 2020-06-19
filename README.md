# Spotify a simple CRUD - from Crescer

This is my first project with [.NET](https://dotnet.microsoft.com/) creating a web service (ASP.NET) in a simple CRUD.

NOTE: This repository is an increment from the base project of [Crescer](https://crescer.cwi.com.br/).

### First steps

Use [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/) or any other IDE or CLI to build and run the WebAPI.

Once running the service access:
- Swagger in: *http://localhost:53651/swagger/*
- Do your calls to: *http://localhost:53651/api/*

### Roadmap

The following are the improvement goals from the base project.

- [x] Add MongoDB to store all data
    - [x] MusicaRepository
    - [x] AlbumRepository
- [x] Enhance RESTful API to follow REST principles (eg: when creating an entity we should return 201 instead of the simple 200)
- [x] Read Mongo connection string from configuration instead of hardcoded
- [ ] Return DTOs instead of entity classes on Controller responses
- [ ] Add new Unit Tests for invalid data such as invalid or missing **ObjectId**