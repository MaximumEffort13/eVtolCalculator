using Application.Commands.Person;
using Application.Queries.Person;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<IdentityUser> _userManager;

        public PersonController(ISender sender, UserManager<IdentityUser> userManager)
        {
            _sender = sender;
            _userManager = userManager;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("/getLoggedInUser")]
        public async Task<IActionResult> GetLoggedInUser(string email, CancellationToken cancellationToken) 
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var query = new GetPersonByUserIdQuery(Guid.Parse(user.Id));
            var response = await _sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
        }

        // GET api/<PersonController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetPersonByUserIdQuery(userId);
            var response = await _sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response) : NotFound();
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand person, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var response = await _sender.Send(person, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
