using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
using MediatR;

namespace ApplicationLayer.TodoItems.Queries
{
    // Query: hämtar alla TodoItems
    public record GetAllTodoItemsQuery : IRequest<List<TodoItem>>;
}