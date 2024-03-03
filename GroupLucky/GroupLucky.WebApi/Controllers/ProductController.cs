using GroupLucky.Application.Features.Categories.Commands;
using GroupLucky.Application.Features.Products.Commands;
using GroupLucky.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroupLucky.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsAll()
        {
            return Ok(await _mediator.Send(new GetProductQuery())); 
        }

        [HttpGet("[action]/{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { ProductId = productId});
            return Ok(product);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ProductSave([FromBody] ProductSaveCommand command)
        {
            return Ok(await _mediator.Send(command));
            //var response = await _mediator.Send(command);

            //if (response.Success)
            //{
            //    return Ok(response);
            //}
            //else
            //{
            //    return BadRequest(response);
            //}
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("[action]/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            return Ok(await _mediator.Send(new ProductDeleteCommand { ProductId = productId}));
        }
    }
}
