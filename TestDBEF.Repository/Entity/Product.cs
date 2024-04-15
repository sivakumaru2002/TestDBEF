using System.ComponentModel.DataAnnotations;

namespace TestDBEF.Repository.Entity
{
    public class Product
    {
        [Key]
        public Guid product_id {get;set;}
        [Required]
        public string productname { get;set;}
        [Required]
        public int quantity { get;set;}
        [Required]
        public long price { get;set;}

    }
}
