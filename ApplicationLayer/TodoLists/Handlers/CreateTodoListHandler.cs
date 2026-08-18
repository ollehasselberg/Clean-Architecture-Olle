using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoLists.Commands;

namespace ApplicationLayer.TodoLists.Handlers
{
    // Handler för CreateTodoListCommand: bygger entiteten och sparar den via repository
    public class CreateTodoListHandler
        : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly ITodoListRepository _repo;

        public CreateTodoListHandler(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(
            CreateTodoListCommand request,
            CancellationToken cancellationToken)
        {
            var list = new TodoList
            {
                Name = request.Name
            };

            await _repo.AddAsync(list);

            return list.Id;
        }
    }
}
