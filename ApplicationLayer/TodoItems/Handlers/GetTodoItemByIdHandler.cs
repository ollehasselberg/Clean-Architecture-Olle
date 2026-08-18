using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoItems.Queries;

namespace ApplicationLayer.TodoItems.Handlers
{
    // Handler för GetTodoItemByIdQuery: hämtar ett enskilt item, null om det inte finns
    public class GetTodoItemByIdHandler
        : IRequestHandler<GetTodoItemByIdQuery, TodoItem?>
    {
        private readonly ITodoItemRepository _repo;

        public GetTodoItemByIdHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<TodoItem?> Handle(
            GetTodoItemByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
