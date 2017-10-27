using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FritoLay.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
