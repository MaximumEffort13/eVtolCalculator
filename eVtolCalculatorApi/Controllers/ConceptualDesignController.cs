using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.ConceptualDesign;
using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Application.Commands.ConceptualDesign;
using Application.Queries.Battery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ConceptualDesignController : ControllerBase
    {
        private readonly ISender _sender;

        public ConceptualDesignController(ISender sender)
        {
            _sender = sender;
        }

        // GET api/<ConceptualDesignController>/5
        [HttpGet]
        [Route("GetConceptualDesign/{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetConceptualDesignByIdQuery(id);
            Result<ConceptualDesignDto> response = await _sender.Send(query, cancellationToken);

            return response.IsSuccess? Ok(response.Value) : NotFound(response.Errors);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllConceptualDesignsQuery();
            var response = await _sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
        }

        // POST api/<ConceptualDesignController>
        [HttpPost]
        [Route("CreateConceptualDesign")]
        public async Task<IActionResult> PostAsync(CreateConceptualDesignCommand command, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var response = await _sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
        }
    }
}
