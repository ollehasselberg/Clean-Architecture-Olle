using MediatR;

namespace ApplicationLayer.TodoLists.Commands
{
    // Command: skapar en ny TodoList, returnerar det nya id:t
    public record CreateTodoListCommand(string Name) : IRequest<int>;
}
