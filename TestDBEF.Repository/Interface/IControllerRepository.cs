

using TestDBEF.Repository.Entity;

namespace TestDBEF.Repository.Interface
{
    public interface IControllerRepository
    {
        Task<List<string>> GetHighSellingProduct();
        Task<List<string>> GetHighPurchased();
        Task<List<string>> GetAllProductMaxPurchased();
        Task<List<Customer>> GetAllCustomer();
        Task<List<Product>> GetAllProduct();
        Task<List<Orders>> GetAllOrder();
    }
}
