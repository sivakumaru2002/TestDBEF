using System.ComponentModel.DataAnnotations;

namespace TestDBEF.Repository.Entity
{
    public class Customer
    {
        [Key]
        public Guid customer_id { get; set; }
        [Required]
        public string customerName { get; set;}
        public long mobile { get; set;}
        [Required]
        public string emailID { get; set;}
        public string gender { get; set;}
    }
}
