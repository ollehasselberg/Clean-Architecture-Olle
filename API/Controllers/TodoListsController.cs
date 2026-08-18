using ApplicationLayer.TodoLists.Commands;
using ApplicationLayer.TodoLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCleanApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTodoListsQuery());
            return Ok(result);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoListCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}
