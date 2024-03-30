using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.listResultDto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly iorderService _orderservice;
        public OrderController(iorderService orderService)
        {
            _orderservice = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<listResultDto<getOrdersWithoutItems>>> GetallOrders()
        {
            return Ok(await _orderservice.GetAllOrders());
        }
        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<listResultDto<GetOrderDto>>> getUserOrders(string email)
        {
            if (email != string.Empty)
            {
                var orders = await _orderservice.getUserOrders(email);
                if (orders is not null)
                {
                    return Ok(orders);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<resultDto<GetOrderDto>>> getOrderById(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                var orders = await _orderservice.getOrderById(orderId);
                if (orders is not null)
                {
                    return Ok(orders);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> CreateOrder(string email, Guid transactionId, Guid sessionId)
        {
            //var _ShoppingCartSessionId = getsessionId();
            if (email != string.Empty || transactionId != Guid.Empty)
            {
                var resultOrder = await _orderservice.createOrder(email, transactionId, sessionId);
                return Ok(resultOrder);
            }
            return BadRequest();
        }

        [HttpPut("{orderId}/{productId}/{quantity}")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> updateOrderItemQuantity(Guid orderId, Guid productId, int quantity)
        {
            if (orderId != Guid.Empty || productId != Guid.Empty || quantity < 0)
            {
                var resultOrder = await _orderservice.updateOrderItemQuantity(orderId, productId, quantity);
                return Ok(resultOrder);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<resultDto<GetOrderDto>>> DeleteOrder(Guid id)
        {
            if (id != Guid.Empty)
            {
                return (await _orderservice.deleteOrder(id));
            }
            return NotFound();
        }
    }
}
