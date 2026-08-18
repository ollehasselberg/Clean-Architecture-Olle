using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoLists.Queries;

namespace ApplicationLayer.TodoLists.Handlers
{
    // Handler för GetAllTodoListsQuery: delegerar rakt av till repository
    public class GetAllTodoListsHandler
        : IRequestHandler<GetAllTodoListsQuery, List<TodoList>>
    {
        private readonly ITodoListRepository _repo;

        public GetAllTodoListsHandler(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TodoList>> Handle(
            GetAllTodoListsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
