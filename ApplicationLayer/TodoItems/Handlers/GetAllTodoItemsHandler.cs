using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoItems.Queries;

namespace ApplicationLayer.TodoItems.Handlers
{
    public class GetAllTodoItemsHandler
        : IRequestHandler<GetAllTodoItemsQuery, List<TodoItem>>
    {
        private readonly ITodoItemRepository _repo;

        public GetAllTodoItemsHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TodoItem>> Handle(
            GetAllTodoItemsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
                //Hämtar data från Repository
        }
    }
}