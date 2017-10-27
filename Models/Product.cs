using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FritoLay.Models
{
    [Table("Products")]
    public class Product : IEquatable<Product>
    {
        public Product()
        {
            this.Reviews = new HashSet<Review>();
        }
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool Featured { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public bool Equals(Product other)
        {
            if (this.ProductId != other.ProductId)
                return false;
            if (this.ProductName != other.ProductName)
                return false;
            if (this.Featured != other.Featured)
                return false;
            if (this.CountryOfOrigin != other.CountryOfOrigin)
                return false;
            return true;
        }
    }
}
