using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly _2B_EgyptDBContext _context;
        public UserRepository(_2B_EgyptDBContext context)
        {
            _context = context;
        }
        private readonly UserManager<MyUser> _userManager;
        public async Task<bool>  addAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindByIdAsync(addressDto.id.ToString());
           if (user == null) { 
            
                if(addressDto.addressLine1!=null)
                    user.addressLine1=addressDto.addressLine1;
                if (addressDto.addressLine2 != null)
                    user.addressLine2=addressDto.addressLine2;
                if (addressDto.country != null)
                    user.country = addressDto.country;
                if (addressDto.city != null)
                    user.city = addressDto.city;

                await _context.SaveChangesAsync();
                return true;

           }
            return false;
        }

    }
}
