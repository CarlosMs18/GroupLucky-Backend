﻿using GroupLucky.Application.Features.Products.Queries;
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
    }
}
