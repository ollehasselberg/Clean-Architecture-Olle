using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    public record UpdateTodoItemCommand(int Id, string Title, bool IsDone) : IRequest<bool>;
}
