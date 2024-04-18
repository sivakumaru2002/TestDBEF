

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
        Task<Product> CreateProduct(Product product);
        Task<Product> DeleteProduct(Product product);
        Task<Product> GetProductById(Guid id);
        Task<Product> UpdateProduct(Product product);
    }
}
