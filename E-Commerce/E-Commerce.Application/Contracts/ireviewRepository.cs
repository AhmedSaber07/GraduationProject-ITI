using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;

namespace E_Commerce.Application.Contracts
{
    public interface ireviewRepository :ibaseRepository<Review,Guid> { }
}
