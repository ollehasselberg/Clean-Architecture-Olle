using ApplicationLayer.TodoLists.Commands;
using ApplicationLayer.TodoLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCleanApi.Api.Controllers
{
    // Minimal CRUD för TodoList - behövs för att kunna skapa en giltig TodoListId till TodoItems
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator) //Injicerar MediatR-mediatorn via DI
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAll() //Hämtar alla to-do-listor inkl. deras items
        {
            var result = await _mediator.Send(new GetAllTodoListsQuery());
            return Ok(result);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoListCommand command) //Skapar en ny to-do-lista och returnerar dess id
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}
