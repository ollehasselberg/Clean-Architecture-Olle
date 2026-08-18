using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoItems.Commands;

namespace ApplicationLayer.TodoItems.Handlers
{
    public class DeleteTodoItemHandler
        : IRequestHandler<DeleteTodoItemCommand, bool>
    {
        private readonly ITodoItemRepository _repo;

        public DeleteTodoItemHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(
            DeleteTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            if (item is null)
            {
                return false;
            }

            await _repo.DeleteAsync(item);
            return true;
        }
    }
}
