using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    // Command: tar bort ett TodoItem, returnerar false om id inte finns
    public record DeleteTodoItemCommand(int Id) : IRequest<bool>;
}
