using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.CartDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class shoppingCartService : ishoppingCartService
    {
    
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;
        public shoppingCartService(  IMapper mapper, IUnitOfWork _unit)
        {
            this._unit = _unit;
            _mapper = mapper;
         
        }

        public async Task<resultDto<CreateOrUpdateDto>> AddTOCart(Guid producid, int quantity, Guid sessionid)
        {
            var product = await _unit.product.GetByIdAsync(producid);
            var allCartData = await  _unit.shoppingCart.GetAllAsync();
            var shoppingCartitem = allCartData.FirstOrDefault(c => c.ProductId == producid && c.sessionId == sessionid);
            if (product.stockQuantity >= quantity)
            {
                if (shoppingCartitem == null)
                {

                    CreateOrUpdateDto AddItemToCart = new CreateOrUpdateDto()
                    {
                        ItemTotal = product.price * quantity,
                        ProductId = producid,
                        Quantity = quantity,
                        SessionId = sessionid
                    };
                    var cartEntity = _mapper.Map<Cart>(AddItemToCart);

                    cartEntity.Id = Guid.NewGuid();
                    cartEntity.createdAt = DateTime.Now;
                    await  _unit.shoppingCart.CreateAsync(cartEntity);

                    await  _unit.shoppingCart.SaveChangesAsync();

                    var cartDto = _mapper.Map<CreateOrUpdateDto>(cartEntity);
                    cartDto.ItemTotal = product.price * quantity;
                    return new resultDto<CreateOrUpdateDto> { Entity = cartDto, IsSuccess = true, Message = "Added Item Successfully" };
                }
                else
                {
                    shoppingCartitem.Quantity += quantity;
                    shoppingCartitem.updatedAt = DateTime.Now;
                    await  _unit.shoppingCart.SaveChangesAsync();

                    var cartDto = _mapper.Map<CreateOrUpdateDto>(shoppingCartitem);
                    cartDto.ItemTotal = product.price * quantity;
                    return new resultDto<CreateOrUpdateDto> { Entity = cartDto, IsSuccess = true, Message = "Item Quantiy increased" };
                }
            }
            else
            {
                var cartDto = _mapper.Map<CreateOrUpdateDto>(shoppingCartitem);
                return new resultDto<CreateOrUpdateDto> { Entity = cartDto, IsSuccess = false, Message = "Quantity is more than Stock Quantity" };
            }
        }
        public async Task<resultDto<GetCartDto>> DeleteCart(Guid sessionId)
        {
            var allDataQuery = await  _unit.shoppingCart.GetAllAsync();
            //var GuidsessionId = Guid.Parse(sessionId);
            var cartItems = await allDataQuery.Where(c => c.sessionId==sessionId).ToListAsync();
            resultDto<GetCartDto> deletedCart = new resultDto<GetCartDto>();

            if (cartItems is null)
            {
                deletedCart.Entity = null;
                deletedCart.IsSuccess = false;
                deletedCart.Message = "Cart Not Found";
                return deletedCart;
            }
            foreach (var item in cartItems)
            {
                 await  _unit.shoppingCart.HardDeleteAsync(item);
            }
            deletedCart.IsSuccess = true;
            deletedCart.Message = "deleted Successfully";
            await  _unit.shoppingCart.SaveChangesAsync();

            return deletedCart;
        }
        public async Task<resultDto<CreateOrUpdateDto>> IncreaseCartProductQuantity(Guid sessionId,Guid productId)
        {
            var product = await _unit.product.GetByIdAsync(productId);
            if (product == null)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "Product Id Not Found" };
            }
            var allCartData = await  _unit.shoppingCart.GetAllAsync();
            var shoppingCartitem = allCartData.FirstOrDefault(c => c.ProductId == productId && c.sessionId == sessionId);

            if (shoppingCartitem == null)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "Cart is Empty" };
            }
           
            shoppingCartitem.Quantity++;
            shoppingCartitem.updatedAt = DateTime.Now;
            await  _unit.shoppingCart.SaveChangesAsync();

            var cartDto = _mapper.Map<CreateOrUpdateDto>(shoppingCartitem);
            return new resultDto<CreateOrUpdateDto> { Entity = cartDto, IsSuccess = true, Message = "Item Quantiy increased" };

        }
        public async Task<resultDto<CreateOrUpdateDto>> DecreaseCartProductQuantity(Guid productId, Guid sessionId)
        {
            var product = await _unit.product.GetByIdAsync(productId);
            if (product == null)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "Product Id Not Found" };
            }
            var allCartData = await  _unit.shoppingCart.GetAllAsync();
            var shoppingCartitem = allCartData.FirstOrDefault(c => c.ProductId == productId && c.sessionId == sessionId);

            if (shoppingCartitem == null)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "Cart is Empty" };
            }
            shoppingCartitem.Quantity--;
            shoppingCartitem.updatedAt = DateTime.Now;
            await  _unit.shoppingCart.SaveChangesAsync();

            var cartDto = _mapper.Map<CreateOrUpdateDto>(shoppingCartitem);
            return new resultDto<CreateOrUpdateDto> { Entity = cartDto, IsSuccess = true, Message = "Item Quantiy increased" };

        }

        public async Task<resultDto<GetCartDto>> RemoveCartItem(Guid productId,Guid sessionId)
        {
            var product = await _unit.product.GetByIdAsync(productId);
            if (product == null)
            {
                return new resultDto<GetCartDto> { Entity = null, IsSuccess = false, Message = "Product Id Not Found" };
            }
            var allCartData = await  _unit.shoppingCart.GetAllAsync();

            if (allCartData == null)
            {
                return new resultDto<GetCartDto> { Entity = null, IsSuccess = false, Message = "Cart is Empty" };
            }
            var shoppingCartitem = allCartData.FirstOrDefault(c => c.ProductId == productId && c.sessionId == sessionId);

            var deleteditemEntity = await  _unit.shoppingCart.HardDeleteAsync(shoppingCartitem);
            await  _unit.shoppingCart.SaveChangesAsync();

            var deleteditemDto=_mapper.Map<GetCartDto>(deleteditemEntity);

            return new resultDto<GetCartDto> { Entity = deleteditemDto, IsSuccess = true, Message = "Item removed successfuly" };
        }


        public async Task<listResultDto<GetCartDto>> GetAllCartItems(Guid sessionId)
        {
            var allDataQuery = await  _unit.shoppingCart.GetAllAsync();
            var cartItemsEntities = await allDataQuery.Include(c=>c.Product).Where(c => c.sessionId == sessionId).ToListAsync();
            decimal cartTotal = 0;
            foreach (var item in cartItemsEntities)
            {
                cartTotal += item.Product.price * item.Quantity;
            }
            var cartItemsDtos = _mapper.Map<IEnumerable<GetCartDto>>(cartItemsEntities);
            return new listResultDto<GetCartDto> { entities = cartItemsDtos, count = cartTotal };
        }

    }
}
