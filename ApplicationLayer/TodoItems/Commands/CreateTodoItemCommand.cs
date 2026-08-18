using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationLayer.TodoItems.Commands
{
    // Command: skapar ett nytt TodoItem, returnerar det nya id:t
    public record CreateTodoItemCommand(string Title, int TodoListId)
        : IRequest<int>;
}