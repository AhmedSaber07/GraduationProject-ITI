using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.OrderDto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {
            
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> CreateOrder(Guid UserId, Guid paymentId)
        {
            if (UserId == Guid.Empty || paymentId == Guid.Empty)
            {
                return Ok();
            }
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        private Guid getsessionId()
        {
            string getSessionId = HttpContext.Session.GetString("SessionCartId");
            if (getSessionId != null)
            {
                return Guid.Parse(getSessionId);
            }
            return Guid.Empty;
        }
    }
}
