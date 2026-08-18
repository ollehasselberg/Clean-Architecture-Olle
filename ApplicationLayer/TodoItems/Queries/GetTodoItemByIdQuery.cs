using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.TodoItems.Queries
{
    // Query: hämtar ett TodoItem via id, null om det inte finns
    public record GetTodoItemByIdQuery(int Id) : IRequest<TodoItem?>;
}
