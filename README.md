# Clean Architecture - Olle

En ASP.NET Core Web API byggd enligt Clean Architecture-principerna, med CQRS (MediatR), Repository Pattern och Entity Framework Core mot SQL Server.

## Arkitektur

Projektet är uppdelat i fyra lager, var och en med ett tydligt ansvar och beroenden som bara pekar inåt:

```
API  -->  ApplicationLayer  -->  DomainLayer
 |                                    ^
 '------> InfrastructureLayer --------'
```

- **DomainLayer** — entiteter (`TodoItem`, `TodoList`) och repository-interfaces (`ITodoItemRepository`, `ITodoListRepository`). Inga beroenden till andra lager.
- **ApplicationLayer** — CQRS: Commands och Queries samt deras MediatR-handlers. Använder bara repository-interfacen från Domain, aldrig Infrastructure direkt.
- **InfrastructureLayer** — `AppDbContext` (EF Core mot SQL Server) och konkreta repository-implementationer.
- **API** — ASP.NET Core Web API. Controllers skickar allt via `IMediator`, ingen affärslogik ligger i controllern.

## Modeller och relation

- `TodoList` (1) → `TodoItem` (många): en `TodoList` har flera `TodoItem`, varje `TodoItem` hör till exakt en `TodoList` (`TodoListId` FK).

## CQRS + MediatR

Commands och Queries ligger separerade under `ApplicationLayer/TodoItems` och `ApplicationLayer/TodoLists` (t.ex. `Commands/CreateTodoItemCommand.cs`, `Queries/GetAllTodoItemsQuery.cs`). Varje controller-action skickar requesten via `IMediator.Send(...)` till motsvarande handler — controllern innehåller ingen affärslogik.

## Repository Pattern

- Interface: `DomainLayer/Interfaces/ITodoItemRepository.cs`, `ITodoListRepository.cs`
- Implementation: `InfrastructureLayer/Repositories/TodoItemRepository.cs`, `TodoListRepository.cs`
- Handlers i ApplicationLayer beror bara på interfacen, aldrig på EF Core direkt.

## Kom igång lokalt

### Krav
- .NET 8 SDK
- SQL Server LocalDB (följer med Visual Studio) eller valfri SQL Server-instans

### 1. Klona och återställ
```bash
git clone https://github.com/ollehasselberg/Clean-Architecture-Olle.git
cd Clean-Architecture-Olle
dotnet restore
```

### 2. Skapa databasen (kör migrations)
Connection string finns i `API/appsettings.json` (`ConnectionStrings:DefaultConnection`), pekar mot lokal `(localdb)\MSSQLLocalDB` som standard.

```bash
dotnet tool install --global dotnet-ef   # om du inte redan har den
dotnet ef database update --project InfrastructureLayer --startup-project API
```

### 3. Starta API:t
```bash
dotnet run --project API
```
Swagger UI öppnas automatiskt på `https://localhost:7114/swagger` (eller `http://localhost:5086/swagger`).

## API-endpoints

| Metod | Endpoint | Beskrivning |
|---|---|---|
| GET | `/api/todolists` | Hämta alla to-do-listor |
| POST | `/api/todolists` | Skapa en ny to-do-lista |
| GET | `/api/todoitems` | Hämta alla to-do-items |
| GET | `/api/todoitems/{id}` | Hämta ett specifikt to-do-item |
| POST | `/api/todoitems` | Skapa ett nytt to-do-item (kräver giltigt `todoListId`) |
| PUT | `/api/todoitems/{id}` | Uppdatera ett to-do-item |
| DELETE | `/api/todoitems/{id}` | Ta bort ett to-do-item |

Full interaktiv dokumentation finns i Swagger UI när projektet körs.

## Gitflöde

`main` är skyddad — inga direkta pushar tillåts, alla ändringar går via en feature-branch och en Pull Request:

```bash
git checkout -b feature/mitt-tillagg
# gör ändringar, committa
git push -u origin feature/mitt-tillagg
gh pr create   # eller skapa PR:en på github.com
```
