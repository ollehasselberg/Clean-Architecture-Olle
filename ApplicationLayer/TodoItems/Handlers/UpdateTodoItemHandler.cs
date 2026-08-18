using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoItems.Commands;

namespace ApplicationLayer.TodoItems.Handlers
{
    public class UpdateTodoItemHandler
        : IRequestHandler<UpdateTodoItemCommand, bool>
    {
        private readonly ITodoItemRepository _repo;

        public UpdateTodoItemHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(
            UpdateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _repo.GetByIdAsync(request.Id);
            if (item is null)
            {
                return false;
            }

            item.Title = request.Title;
            item.IsDone = request.IsDone;

            await _repo.UpdateAsync(item);
            return true;
        }
    }
}
