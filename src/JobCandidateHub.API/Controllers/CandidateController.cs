using JobCandidateHub.Application.Command;
using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHub.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCandidate([FromBody] CreateOrUpdateCandidateDto candidate)
        {
            var result =  await _mediator.Send(new CreateOrUpdateCandidateCommand(candidate));
            return Ok(result);
        }
    }
}
