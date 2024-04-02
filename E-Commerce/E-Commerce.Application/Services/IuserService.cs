using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.BrandDto;
using E_Commerce.Domain.DTOs.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public interface IuserService
    {
        Task<bool> AddAddress(AddressDto addressDto);

        Task<bool> DeleteAddress(string Email);
    }
}
