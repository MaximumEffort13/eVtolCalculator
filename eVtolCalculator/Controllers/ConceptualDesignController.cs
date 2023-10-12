using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Application.ConceptualDesignFacilitators.Commands;
using Application.ConceptualDesignFacilitators.Queries;
using Application.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptualDesignController : ControllerBase
    {
        private readonly ISender _sender;

        public ConceptualDesignController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<ConceptualDesignController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ConceptualDesignController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetConceptualDesignByIdQuery(id);
            Result<ConceptualDesignDto> response = await _sender.Send(query, cancellationToken);

            return response.IsSuccess? Ok(response.Value) : NotFound(response.Errors) ;
        }

        // POST api/<ConceptualDesignController>
        [HttpPost]
        public async Task<IActionResult> Post(double totalDesignWeight, double totalPayloadWeight, int flightTimeInMinutes)
        {
            CreateConceptualDesignCommand overallDesign = new (totalDesignWeight, totalPayloadWeight, flightTimeInMinutes);
            var response = await _sender.Send(overallDesign);

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
        }

        // PUT api/<ConceptualDesignController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConceptualDesignController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
