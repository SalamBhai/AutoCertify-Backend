using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Services.Commands;
using Application.Services.Queries;
using Application.Services.Queries.Training;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingController : ControllerBase
{
   private readonly IMediator _mediatr;

    public TrainingController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
     [HttpPost("create")]
    public async Task<IActionResult> RegisterTrainingAsync([FromBody] CreateRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("update")]
    public async Task<IActionResult> UpdateTrainingAsync([FromBody] UpdateTrainingRequest request)
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("delete")]
    public async Task<IActionResult> DeleteTrainingAsync([FromBody] DeleteTrainingRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("getall")]
    public async Task<IActionResult> GetTrainingsAsync([FromQuery] GetAll.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("paginated")]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] GetAllWithPagination.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
    
     [HttpGet("id")]
    public async Task<IActionResult> GetTrainingById([FromQuery] GetTrainingById.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
    
    [HttpGet("name")]
    public async Task<IActionResult> GetTrainingByName([FromQuery] GetTrainingByName.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
}
