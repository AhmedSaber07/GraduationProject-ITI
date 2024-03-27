using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.CartDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ishoppingCartService _cartService;
        public CartController(ishoppingCartService cartService)
        {
            _cartService = cartService;
        }
        // GET: api/<CartController>
        //[HttpGet()]


        // GET api/<CartController>/5
        [HttpGet]
        public async Task<ActionResult<listResultDto<GetCartDto>>> Get()
        {
            var _ShoppingCartSessionId = getsessionId();
            if (_ShoppingCartSessionId != Guid.Empty)
            {
                var RemovedProduct = await _cartService.GetAllCartItems(_ShoppingCartSessionId);
                return Ok(RemovedProduct);
            }
            return NotFound();
        }

        [HttpPost(Name = "AddItemToCart")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> AddItemToCart(Guid productid, int quantity)
        {
            var _ShoppingCartSessionId = createIfNullsessionId();

            var resultCartitem = await _cartService.AddTOCart(productid, quantity, _ShoppingCartSessionId);
            return Created("Product", resultCartitem);
            //if(ModelState.IsValid)
            //{
            //}
        }

        // PUT api/<CartController>/5
        [HttpPut("IncreaseQuantity")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> IncreaseQuantity(Guid productId)
        {
            var _ShoppingCartSessionId = getsessionId();
            if (_ShoppingCartSessionId != Guid.Empty)
            {
                var increasedProduct = await _cartService.IncreaseCartProductQuantity(_ShoppingCartSessionId, productId);
                return Created("Product", increasedProduct);
            }
            return NotFound();
        }
        [HttpPut("DecreaseQuantity")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> DecreaseQuantity(Guid productId)
        {
            var _ShoppingCartSessionId = getsessionId();
            if (_ShoppingCartSessionId != Guid.Empty)
            {
                var decreasedProduct = await _cartService.DecreaseCartProductQuantity(productId, _ShoppingCartSessionId);
                return Created("Product", decreasedProduct);
            }
            return NotFound();
        }
        [HttpPut("RemoveItem")]
        public async Task<ActionResult<resultDto<GetCartDto>>> RemoveItem(Guid productId)
        {

            var _ShoppingCartSessionId = getsessionId();
            if (_ShoppingCartSessionId != Guid.Empty)
            {
                var RemovedProduct = await _cartService.RemoveCartItem(productId, _ShoppingCartSessionId);
                return Created("Product", RemovedProduct);
            }
            return NotFound();
        }

        // DELETE api/<CartController>/5
        [HttpDelete("DeleteCart")]
        public async Task<ActionResult<resultDto<GetCartWithProductsDto>>> DeleteCart()
        {
            var _ShoppingCartSessionId = getsessionId();

            var cart = await _cartService.DeleteCart(_ShoppingCartSessionId);
            if (cart is not null)
            {
                return Ok(cart);
            }
            else
            {
                return NotFound();
            }
        }

        private Guid createIfNullsessionId()
        {
            string getSessionId = HttpContext.Session.GetString("SessionCartId") ?? Guid.NewGuid().ToString();
            HttpContext.Session.SetString("SessionCartId", getSessionId);
            return Guid.Parse(getSessionId);
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
