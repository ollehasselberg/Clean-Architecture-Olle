using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ApplicationLayer.TodoItems.Commands;

namespace ApplicationLayer.TodoItems.Handlers
{
    // Handler för CreateTodoItemCommand: bygger entiteten och sparar den via repository
    public class CreateTodoItemHandler
        : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly ITodoItemRepository _repo;

        public CreateTodoItemHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(
            CreateTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = new TodoItem
            {
                Title = request.Title,
                TodoListId = request.TodoListId,
                IsDone = false
            };

            await _repo.AddAsync(item);

            return item.Id;
        }
    }
}