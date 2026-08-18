using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    public record CreateTodoItemCommand(string Title, int TodoListId)
        : IRequest<int>;
}