using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Week_2_Task_1.dto;
using Week_2_Task_1.interfaces;

namespace Week_2_Task_1.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly iservices _service;

        public ProductsController(iservices service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(prod_responese), 201)]
        [ProducesResponseType(400)]
        public ActionResult<prod_responese> Create(
            [FromBody] create_product dto)
        {
            try
            {
                var createdProduct = _service.add_product(dto);

                return CreatedAtAction(nameof(GetById), new { id = createdProduct.id },createdProduct);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<prod_responese>), 200)]
        public ActionResult<List<prod_responese>> GetAll()
        {
            return Ok(_service.get_products());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(prod_responese), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<prod_responese> GetById([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    new { message = "Id must be greater than zero." });
            }

            var product = _service.get_product_by_id(id);

            if (product is null)
            {
                return NotFound(
                    new { message = "Product not found." });
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] update_prod dto)
        {
            if (id <= 0)
            {
                return BadRequest(
                    new { message = "Id must be greater than zero." });
            }

            try
            {
                bool updated = _service.update_product(id, dto);

                if (!updated)
                {
                    return NotFound(
                        new { message = "Product not found." });
                }

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    new { message = "Id must be greater than zero." });
            }

            bool deleted = _service.delete_product(id);

            if (!deleted)
            {
                return NotFound(
                    new { message = "Product not found." });
            }

            return NoContent();
        }
    }
}