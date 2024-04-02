using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;

namespace E_Commerce.Infrastructure.Repositories
{
    public class ReviewRepository : baseRepository<Review,Guid>,ireviewRepository
    {
        public ReviewRepository(_2B_EgyptDBContext context) : base(context){}
    }
}
