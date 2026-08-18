# Clean Architecture - Olle

Detta är min inlämningsuppgift i skolan! Det är en To-Do-list-API byggd i ASP.NET Core (.NET 8) som följer Clean Architecture. Man kan skapa listor och lägga till/ändra/ta bort saker i dem.

## Vad är Clean Architecture (kort förklarat)

Grejen med Clean Architecture är att man delar upp koden i olika lager så att allt inte ligger hugget i sten i samma fil. Jag har fyra projekt/lager:

- **DomainLayer** - detta är kärnan, här ligger bara mina entiteter (`TodoItem` och `TodoList`) och interfaces för mina repositories. Detta lager beror inte på något annat, det är typ "hjärtat".
- **ApplicationLayer** - här ligger all logik för vad som ska hända, uppdelat i Commands (gör något, t.ex skapa) och Queries (hämta något). Jag använder MediatR-paketet för att koppla ihop dessa.
- **InfrastructureLayer** - här pratar jag med databasen. Har min `AppDbContext` (EF Core) och de "riktiga" implementationerna av repository-interfacen från Domain.
- **API** - detta är själva webb-API:et som man startar. Controllers här gör egentligen inte mycket, de bara skickar vidare requesten till MediatR som hittar rätt handler.

Tanken är att man ska kunna byta ut t.ex databasen utan att behöva ändra i Domain/Application, för de vet inte ens att SQL Server finns.

## Modellerna

Jag har två entiteter:
- `TodoList` - en lista med ett namn
- `TodoItem` - en sak att göra, hör till en lista (`TodoListId`)

Så en lista kan ha flera items, det är alltså en 1-till-många-relation.

## CQRS + MediatR

Alla "actions" är antingen ett **Command** (ändrar något, t.ex `CreateTodoItemCommand`) eller en **Query** (hämtar något, t.ex `GetAllTodoItemsQuery`). De ligger i egna mappar under `ApplicationLayer` så det är lätt att hitta. Controllern skickar bara `_mediator.Send(command)` och väntar på svar, ingen logik ligger i controllern.

## Repository Pattern

Interfacen (`ITodoItemRepository`, `ITodoListRepository`) ligger i `DomainLayer/Interfaces`, och de faktiska klasserna som pratar med EF Core ligger i `InfrastructureLayer/Repositories`. Handlers i ApplicationLayer känner bara till interfacet, inte hur det faktiskt är implementerat.

## Hur man kör projektet

### Man behöver
- .NET 8 SDK
- SQL Server LocalDB (den brukar följa med om man har Visual Studio installerat)

### Steg 1 - klona ner det
```bash
git clone https://github.com/ollehasselberg/Clean-Architecture-Olle.git
cd Clean-Architecture-Olle
dotnet restore
```

### Steg 2 - skapa databasen
Connection stringen finns i `API/appsettings.json`, den pekar mot en lokal LocalDB som heter `TodoDb`.

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project InfrastructureLayer --startup-project API
```

### Steg 3 - kör igång API:t
```bash
dotnet run --project API
```
Då öppnas Swagger automatiskt i webbläsaren där man kan testa alla endpoints direkt.

## Endpoints

| Metod | URL | Vad den gör |
|---|---|---|
| GET | `/api/todolists` | hämta alla listor |
| POST | `/api/todolists` | skapa en ny lista |
| GET | `/api/todoitems` | hämta alla items |
| GET | `/api/todoitems/{id}` | hämta ett item |
| POST | `/api/todoitems` | skapa ett nytt item (behöver ett giltigt `todoListId`) |
| PUT | `/api/todoitems/{id}` | uppdatera ett item |
| DELETE | `/api/todoitems/{id}` | ta bort ett item |

## Git-grejer

`main`-branchen är skyddad, så jag kan inte pusha direkt dit av misstag - allt måste gå via en Pull Request. Så här gör man en ändring:

```bash
git checkout -b feature/nagot-jag-vill-lagga-till
# gör ändringarna, committa dem
git push -u origin feature/nagot-jag-vill-lagga-till
# skapa PR på github.com och merga den
```
