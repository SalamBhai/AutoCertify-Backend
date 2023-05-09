using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Services.Commands;
using Application.Services.Queries.Participant;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
   private readonly IMediator _mediatr;

    public ParticipantController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
     [HttpPost("create")]
    public async Task<IActionResult> RegisterParticipantAsync([FromBody] CreateParticipantRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("update")]
    public async Task<IActionResult> UpdateParticipantAsync([FromBody] UpdateRequest request)
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("delete")]
    public async Task<IActionResult> DeleteParticipantAsync([FromBody] DeleteRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("getall")]
    public async Task<IActionResult> GetParticipantsAsync([FromQuery] GetAllParticipants.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("paginated")]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] GetAllParticipantsWithPagination.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     
     [HttpGet("certificateNumber")]
    public async Task<IActionResult> GetParticipantByCertificateNumber([FromQuery] GetParticipantByCertificateNumber.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("id")]
    public async Task<IActionResult> GetParticipantById([FromQuery] GetParticipantById.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
    [HttpGet("name")]
    public async Task<IActionResult> GetParticipantByName([FromQuery] GetParticipantByName.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
}
