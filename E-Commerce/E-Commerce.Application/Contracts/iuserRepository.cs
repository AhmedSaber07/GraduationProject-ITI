using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface iuserRepository : ibaseRepository<MyUser, Guid>
    {
        Task<bool> addAddress(AddressDto addressDto);
        Task<bool> DeleteAddress(string Email);
    }
}
