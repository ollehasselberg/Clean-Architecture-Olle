using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.TodoLists.Queries
{
    // Query: hämtar alla TodoLists inkl. deras TodoItems
    public record GetAllTodoListsQuery : IRequest<List<TodoList>>;
}
