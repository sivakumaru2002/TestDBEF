

using TestDBEF.Repository.Entity;

namespace TestDBEF.Service.Interface
{
    public interface IControllerService
    {
        Task<List<string>> GetHighSellingProduct();
        Task<List<string>> GetHighPurchased();
        Task<List<string>> GetAllProductMaxPurchased();
        Task<List<Customer>> GetAllCustomer();
        Task<List<Product>> GetAllProduct();
        Task<List<Orders>> GetAllOrder();
        Task<Product> UpdateProduct(Product product);
        Task<Product> CreateProduct(Product product);
        Task<Product> DeleteProduct(Guid Id);
        Task<Product> GetProductByID(Guid id);
    }
}
