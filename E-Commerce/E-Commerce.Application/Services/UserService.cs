using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.UserAccount;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Services
{
    public class UserService : IuserService
    {
      
        private readonly iuserRepository userRepository;
        public UserService(iuserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> AddAddress(AddressDto addressDto)
        {
            return( await  userRepository.addAddress(addressDto));
          

        }
    }
}
