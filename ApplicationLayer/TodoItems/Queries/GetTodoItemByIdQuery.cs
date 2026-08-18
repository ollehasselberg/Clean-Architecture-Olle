using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.TodoItems.Queries
{
    public record GetTodoItemByIdQuery(int Id) : IRequest<TodoItem?>;
}
