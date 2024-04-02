using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ibrandRepository brand { get; }  
        icategoryRepository category { get; }
        iproductRepository product { get; }
        ishoppingCartRepository shoppingCart { get; }
        iorderRepository order { get; }
        iorderItemRepository orderItem { get; }
        ipaymentRepository payment { get; }
        Task<bool> Complete();

    }
}
