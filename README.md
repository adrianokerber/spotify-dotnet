# Spotify a simple CRUD - from Crescer

This is my first project with [.NET](https://dotnet.microsoft.com/) creating a web service (ASP.NET) in a simple CRUD.

NOTE: This repository is an increment from the base project of [Crescer](https://crescer.cwi.com.br/).

### Roadmap

The following are the improvement goals from the base project.

- [x] Add MongoDB to store all data
    - [x] MusicaRepository
    - [x] AlbumRepository
- [x] Enhance RESTful API to follow REST principles (eg: when creating an entity we should return 201 instead of the simple 200)
- [x] Read Mongo connection string from configuration instead of hardcoded
- [ ] Change MongoAdapter to use inheritage. Each children class should return the DB connection via DI
- [ ] Add relations on MongoDB
- [ ] Return DTOs instead of entity classes on Controller responses
- [ ] Add new Unit Tests for invalid data such as invalid or missing **ObjectId**