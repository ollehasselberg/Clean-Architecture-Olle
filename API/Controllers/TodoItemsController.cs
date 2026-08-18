using ApplicationLayer.TodoItems.Commands;
using ApplicationLayer.TodoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyCleanApi.Api.Controllers
{
    // Full CRUD för TodoItem - alla anrop går via MediatR till en Command/Query-handler i ApplicationLayer
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

            //GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) //HTTP GET-Endpoint - hämtar ett specifikt to-do item
        {
            var result = await _mediator.Send(new GetTodoItemByIdQuery(id));
            return result is null ? NotFound() : Ok(result); //404 om det inte finns, annars 200 med item
        }

            //POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoItemCommand command) //HTTP POST-Endpoint - skapa nytt to-do item och spara den
        {
            var id = await _mediator.Send(command); //Metod för att skicka command och invänta svar
            return Ok(id); //Returnerar resultat (ID) tillbaka till klienten
        }

            //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTodoItemCommand command) //HTTP PUT-Endpoint - uppdaterar ett befintligt to-do item
        {
            if (id != command.Id)
            {
                return BadRequest("Id in route must match Id in body."); //Skydd mot att id i URL och body inte stämmer överens
            }

            var updated = await _mediator.Send(command);
            return updated ? NoContent() : NotFound(); //204 vid lyckad uppdatering, 404 om item inte hittades
        }

            //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) //HTTP DELETE-Endpoint - tar bort ett to-do item
        {
            var deleted = await _mediator.Send(new DeleteTodoItemCommand(id));
            return deleted ? NoContent() : NotFound(); //204 vid lyckad borttagning, 404 om item inte hittades
        }
    }
}