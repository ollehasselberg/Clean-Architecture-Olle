using MediatR;

namespace ApplicationLayer.TodoLists.Commands
{
    public record CreateTodoListCommand(string Name) : IRequest<int>;
}
