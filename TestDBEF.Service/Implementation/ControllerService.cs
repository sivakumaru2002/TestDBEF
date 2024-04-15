using TestDBEF.Service.Interface;
using TestDBEF.Repository.Interface;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using TestDBEF.Repository.Entity;

namespace TestDBEF.Service.Implementation
{
    public class ControllerService : IControllerService
    {
        private readonly IControllerRepository _controllerRepository;
        private readonly IMemoryCache _memoryCache;
        public ControllerService(IControllerRepository controllerRepository,IMemoryCache memoryCache) {
            _controllerRepository = controllerRepository;
            _memoryCache = memoryCache;
        }
        public async Task<List<Customer>> GetAllCustomer()
        {
            if (!_memoryCache.TryGetValue("AllCustomer", out List<Customer> customers))
            {
                customers = await _controllerRepository.GetAllCustomer();
                var CacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024,
                };
                _memoryCache.Set("AllCustomer", customers, CacheOptions);
            }
            return customers;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            if (!_memoryCache.TryGetValue("Product-All", out List<Product> product))
            {
                product = await _controllerRepository.GetAllProduct();
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _memoryCache.Set("Product-All", product, cacheOptions);
            }
            return product;
        }

        public async Task<List<Orders>> GetAllOrder()
        {
            if (!_memoryCache.TryGetValue("Order-All", out List<Orders> order))
            {
                order = await _controllerRepository.GetAllOrder();
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _memoryCache.Set("Order-All", order, cacheOptions);
                _memoryCache.Remove("Order-All");
            }
            return order;
        }
        public async Task<List<string>> GetHighSellingProduct()
        {

            if (!_memoryCache.TryGetValue("Product", out List<string> products))
            {
                products=await _controllerRepository.GetHighSellingProduct();
                var CacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024,
                };
                _memoryCache.Set("Product", products, CacheOptions);
            }
            return products; 
        }
        public async Task<List<string>> GetHighPurchased()
        {
            if (!_memoryCache.TryGetValue("Customer", out List<string> customers))
            {
                customers = await _controllerRepository.GetHighPurchased();
                var CacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024,
                };
                _memoryCache.Set("Customer", customers, CacheOptions);
            }
            return customers;
        }
        public async Task<List<string>> GetAllProductMaxPurchased()
        {
            if (!_memoryCache.TryGetValue("products-Quantity", out List<string> products))
            {
                products = await _controllerRepository.GetAllProductMaxPurchased();
                var CacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024,
                };
                _memoryCache.Set("Customer", products, CacheOptions);
            }
            return products;
        }
    }
}
