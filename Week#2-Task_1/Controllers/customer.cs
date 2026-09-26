using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Week_2_Task_1.dto;
using Week_2_Task_1.interfaces;

namespace Week_2_Task_1.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly iservices _service;

        public CustomersController(iservices service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(response_cus), 201)]
        [ProducesResponseType(400)]
        public ActionResult<response_cus> Create(
            [FromBody] create_customer dto)
        {
            try
            {
                var createdCustomer = _service.add_customer(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdCustomer.id },
                    createdCustomer);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<response_cus>), 200)]
        public ActionResult<List<response_cus>> GetAll()
        {
            return Ok(_service.get_customers());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(response_cus), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<response_cus> GetById(
            [FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    new { message = "Id must be greater than zero." });
            }

            var customer = _service.get_customer_by_id(id);

            if (customer is null)
            {
                return NotFound(
                    new { message = "Customer not found." });
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] update_customer dto)
        {
            if (id <= 0)
            {
                return BadRequest(
                    new { message = "Id must be greater than zero." });
            }

            try
            {
                bool updated = _service.update_customer(id, dto);

                if (!updated)
                {
                    return NotFound(
                        new { message = "Customer not found." });
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

            bool deleted = _service.delete_customer(id);

            if (!deleted)
            {
                return NotFound(
                    new { message = "Customer not found." });
            }

            return NoContent();
        }
    }
}