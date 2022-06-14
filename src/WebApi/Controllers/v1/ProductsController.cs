using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllProductsQuery());
            if (result == null)
                return NotFound("Products don't exist.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetProductByIdQuery { Id = id });
            if (result == null)
                return NotFound("Product doesn't exist.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteProductByIdCommand { Id = id });
            if (result == 0)
                return NotFound("Product doesn't exist to delete.");
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await Mediator.Send(command);
            if (result == 0)
                return BadRequest("Product doesn't exist to update.");
            return Ok(result);
        }
    }
}
