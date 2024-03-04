using GroupLucky.Application.Features.Categories.Commands;
using GroupLucky.Application.Features.Categories.Queries;
using GroupLucky.Application.Features.Products.Commands;
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
        public async Task<ActionResult> GetCategoryAll()
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

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = categoryId });
            return Ok(category);    
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
