using FritoLay.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FritoLay.Models
{
    public class EFProductRepository : IProductRepository
    {
        private FritoLayContext context;

        public static EFProductRepository Instance = new EFProductRepository();

        public EFProductRepository(FritoLayContext context = null)
        {
            if (context == null)
            {
                this.context = new FritoLayContext();
            }
            else
            {
                this.context = context;
            }
        }

        public IQueryable<Product> Products
        { get { return context.Products; } }

        public Product Save(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public void Remove(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public Product Edit(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
            return product;
        }

        public Review Save(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
            return review;
        }
    }
}
