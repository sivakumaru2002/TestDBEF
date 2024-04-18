using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDBEF.Repository.Entity;
using TestDBEF.Repository.Interface;

namespace TestDBEF.Repository.Implementation
{
    public class ControllerRepository : IControllerRepository
    {
        private AppDbContext _DbContext;
        public ControllerRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<List<Customer>> GetAllCustomer()
        {
            List<Customer> customer = await _DbContext.Customer.ToListAsync();               
            return customer;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            List<Product> product = await _DbContext.Product.ToListAsync();
            return product;
        }

        public async Task<List<Orders>> GetAllOrder()
        {
            List<Orders> order = _DbContext.Orders.ToList();
            return order;
        }
            public async Task<List<string>> GetHighSellingProduct()
        {

            List<string> products = new List<string>();
            var topOrderCountSubquery = _DbContext.Orders
                                       .GroupBy(o => o.product_id)
                                       .Select(g => g.Count())
                                       .Max();

            products = _DbContext.Product
                       .Where(product => _DbContext.Orders
                       .Where(o => o.product_id == product.product_id)
                       .Count() == topOrderCountSubquery)
                       .Select(product => product.productname)
                       .ToList();
            return products;
        }

        public async Task<List<string>> GetHighPurchased()
        {
            var topCustomerCount = _DbContext.Orders
                       .GroupBy(ord => ord.customer_id)
                       .Select(g => g.Count())
                       .Max();

            List<string> topCustomers = new List<string>();
            topCustomers = _DbContext.Customer
                       .Where(customer => _DbContext.Orders
                       .Where(o => o.customer_id == customer.customer_id)
                       .Count() == topCustomerCount)
                       .Select(customer => customer.customerName)
                       .ToList(); ;
            return topCustomers;
        }

        public async Task<List<string>> GetAllProductMaxPurchased()
        {
            var maxQuantity = _DbContext.Orders
                               .GroupBy(order => order.product_id)
                               .Select(group => group.Sum(o => o.quantity))
                               .Max();

            List<string> productNames = new List<string>();
            productNames = _DbContext.Product
                .Where(product => _DbContext.Orders
                    .Where(order => order.product_id == product.product_id)
                    .Sum(order => order.quantity) == maxQuantity
                )
                .Select(product => product.productname).ToList();


            return productNames;
        }

        public async Task<Product> CreateProduct(Product product)
        {
             _DbContext.Product.Add(product);
            await _DbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(Product product)
        {
            _DbContext.Product.Remove(product);
            await _DbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _DbContext.Product.FindAsync(id);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _DbContext.Product.Update(product);
            await _DbContext.SaveChangesAsync();
            return product;
        }
    }
}
