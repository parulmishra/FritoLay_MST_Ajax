using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FritoLay.Models.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Product Save(Product product);
        void Remove(Product product);
        Product Edit(Product product);
        Review Save(Review review);
    }
}
