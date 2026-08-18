using ApplicationLayer.TodoItems.Commands;
using ApplicationLayer.TodoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCleanApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator) //Skapar en mediator som tar emot API requests
        {
            _mediator = mediator; 
        }

            // GET
        [HttpGet]
        public async Task<IActionResult> GetAll() //HTTP GET-Endpoint  - API Läser data och inväntar svar
        {
            var result = await _mediator.Send(new GetAllTodoItemsQuery()); //Metod för att skicka API request och invänta svar
            return Ok(result); //Returnerar resultat tillbaka till klienten
        }

            //POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoItemCommand command) //HTTP POST-Endpoint - skapa nytt to-do item och spara den
        {
            var id = await _mediator.Send(command); //Metod för att skicka command och invänta svar
            return Ok(id); //Returnerar resultat (ID) tillbaka till klienten
        }
    }
}