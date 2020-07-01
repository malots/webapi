# WebAPI
This package is intended to assist in the creation of APIs using dotnet core.

Basically it has the following usage scheme:
 - The application layer receives a viewmodel and sends a workmodel to the business layer, it works on top of that object and sends a repository to the repository when necessary.

The Project Structure consists of:
- Application => Application layer where it works with viewmodels and workmodels.
- Business => Business layer where the business rules are, it works with workmodels and repositorymodels.
- Domain => Here are all interfaces, entities, viewmodels and base workmodels. Also enumerators and other classes or objects that make sense to be used by all other layers.
- Infra => Here are the repositories, mappings of the entities, base contexts i.e. everything related to infra or data.

The project structure is a well-known structure, a difference between other projects is that the project works with 3 mappings between objects that are the ViewModels, WorkModels, and RepositoryModels.
