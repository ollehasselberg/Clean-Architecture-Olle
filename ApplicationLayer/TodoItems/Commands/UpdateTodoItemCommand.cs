using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    // Command: uppdaterar titel/status på ett TodoItem, returnerar false om id inte finns
    public record UpdateTodoItemCommand(int Id, string Title, bool IsDone) : IRequest<bool>;
}
