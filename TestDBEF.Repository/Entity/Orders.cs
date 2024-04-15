using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDBEF.Repository.Entity
{
    public class Orders
    {
        [Key]
        public Guid order_id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public Guid customer_id { get; set;}
        [Required]
        [ForeignKey("Product")]
        public Guid product_id { get; set;}
        [Required]
        public int quantity { get; set;}

        public DateOnly Dt {  get; set;}

        public virtual Customer Customer { get; set;}
        public virtual Product Product { get; set;}
    }
}
