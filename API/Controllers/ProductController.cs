using API.Application.Commands.CreateProduct;
using API.Application.Commands.EditProduct;
using API.DTO;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IQueries<Product> _queries;

        public ProductController(IMediator mediator, IQueries<Product> queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Products()
        {
            var result = await _queries.GetAllAsync();
            return Ok(new GetProductsResponseDTO(result));
        }

        [HttpGet]
        [Route("{productId}")]
        [Authorize]
        public async Task<IActionResult> Product([FromRoute] Guid productId)
        {
            var result = await _queries.GetByIdAsync(productId);
            return Ok(new GetProductResponseDTO(result));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Product([FromBody] CreateProductCommand createProductCommand)
        {
            await _mediator.Send(createProductCommand);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPatch]
        [Authorize]
        public async Task <IActionResult> Product([FromBody] EditProductCommand editProductCommand)
        {
            await _mediator.Send(editProductCommand);
            return Ok();
        }
    }
}
