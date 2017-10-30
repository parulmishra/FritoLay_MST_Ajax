using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FritoLay.Models
{
    class TestDbContext: FritoLayContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=3306;database=FritoLay_test;uid=parul;pwd=parul;");
        }
    }
}
