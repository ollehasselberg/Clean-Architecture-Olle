using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    public record DeleteTodoItemCommand(int Id) : IRequest<bool>;
}
