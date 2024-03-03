using GroupLucky.Application.Features.Categories.Commands;
using GroupLucky.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupLucky.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCoursesAll()
        {
            return Ok(await _mediator.Send(new GetCategoryQuery()));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CategorySaveCommandResponse>> CreateCategory([FromBody] CategorySaveCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
