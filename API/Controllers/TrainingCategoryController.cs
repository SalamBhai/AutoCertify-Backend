using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Services.Commands;
using Application.Services.Queries;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingCategoryController : ControllerBase
{
   private readonly IMediator _mediatr;

    public TrainingCategoryController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
     [HttpPost("create")]
    public async Task<IActionResult> RegisterTrainingCategoryAsync([FromBody] CreateTrainingCategoryRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("update")]
    public async Task<IActionResult> UpdateTrainingCategoryAsync([FromBody] UpdateTrainingCategoryRequest request)
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpPut("delete")]
    public async Task<IActionResult> DeleteTrainingCategoryAsync([FromBody] DeleteTrainingCategoryRequest request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("getall")]
    public async Task<IActionResult> GetTrainingCategoriesAsync([FromQuery] GetAllTrainingCategories.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
     [HttpGet("paginated")]
    public async Task<IActionResult> GetAllWithPagination([FromQuery] GetAllTrainingCategoriesWithPagination.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
   
     [HttpGet("id")]
    public async Task<IActionResult> GetTrainingCategoryById([FromQuery] GetTrainingCategoryById.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
    [HttpGet("name")]
    public async Task<IActionResult> GetTrainingCategoryByName([FromQuery] GetTrainingCategoryByName.Request request )
    {
       var response = await _mediatr.Send(request);
       return response.Succeeded? Ok(response) : BadRequest(response);
    }
    
}
