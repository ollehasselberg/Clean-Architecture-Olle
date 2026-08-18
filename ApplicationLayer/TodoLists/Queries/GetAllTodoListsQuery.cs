using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.TodoLists.Queries
{
    public record GetAllTodoListsQuery : IRequest<List<TodoList>>;
}
