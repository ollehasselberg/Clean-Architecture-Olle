# Clean Architecture - Olle

Inlämningsuppgift: "Clean Architecture Web API".

## Om Clean Architecture

Med Clean Architecture delar man upp koden i olika lager för att få bättre struktur och förenkla underhåll. Jag har använt mig av fyra lager:

- **DomainLayer** - detta är kärnan, här ligger bara mina entiteter (`TodoItem` och `TodoList`) och interfaces för mina repos. Detta lager är oberoende av andra lager.
- **ApplicationLayer** - här ligger all logik för vad som ska hända, uppdelat i Commands (gör något, t.ex skapa) och Queries (hämta något). Jag kopplar ihop dessa med hjälp av MediatR-paketet.
- **InfrastructureLayer** - pratar med databasen. Har min `AppDbContext` (EF Core) och de "riktiga" implementationerna av repository-interfacen från Domain.
- **API** - detta är själva webb-API:et som man startar. Controllers här gör egentligen inte mycket, de bara skickar vidare requesten till MediatR som hittar rätt handler.

Tanken är att man ska kunna byta ut t.ex databasen utan att behöva ändra i Domain/Application, för de vet inte ens att SQL Server finns.

## Modellerna

Jag har två entiteter:
- `TodoList` - en lista med ett namn
- `TodoItem` - en sak att göra, hör till en lista (`TodoListId`)

**en lista kan ha flera items**, det är alltså en 1-till-många-relation.

## CQRS + MediatR

Alla "actions" är antingen ett **Command** (ändrar något, t.ex `CreateTodoItemCommand`) eller en **Query** (hämtar något, t.ex `GetAllTodoItemsQuery`). De ligger i egna mappar under `ApplicationLayer` så det är lätt att hitta. Controllern skickar bara `_mediator.Send(command)` och väntar på svar.

## Repository Pattern

Interfacen (`ITodoItemRepository`, `ITodoListRepository`) ligger i `DomainLayer/Interfaces`, och de faktiska klasserna som pratar med EF Core ligger i `InfrastructureLayer/Repositories`. Handlers i ApplicationLayer känner bara till interfacet, inte **hur** det faktiskt är implementerat.

## Hur man kör projektet

### Krav
- .NET 8 SDK
- SQL Server LocalDB

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

## GitHub Branches

`main`-branchen är skyddad, så jag kan inte pusha direkt dit av misstag - allt måste gå via en Pull Request. Så här gör man en ändring:


