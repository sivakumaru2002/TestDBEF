using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDBEF.Repository;
using TestDBEF.Repository.Entity;
using TestDBEF.Service.Interface;

namespace TestDBEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        private readonly IControllerService _ControllerService;
        private readonly ICacheProvider _cacheProvider;
        private readonly IMemoryCache memoryCache;
        public AllController(AppDbContext dbContext,ICacheProvider cacheProvider,IMemoryCache memory,IControllerService controllerService)
        {
            _DbContext = dbContext;           
            _cacheProvider = cacheProvider;
            memoryCache = memory;
            _ControllerService = controllerService;
        }
        [HttpGet]
        [Route("/GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            List<Customer> customer = await _ControllerService.GetAllCustomer();
            return Ok(customer);
        }
        [HttpGet]
        [Route("/GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            List<Product> product = await _ControllerService.GetAllProduct();
            return Ok(product);
        }
        [HttpGet]
        [Route("/GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            return Ok(await _ControllerService.GetAllOrder());
        }

        [HttpGet]
        [Route("/GetHighSellingProduct")]
        public async Task<IActionResult> GetHighSellingProduct()
        {
            List<string> products = new List<string>();
            products=await _ControllerService.GetHighSellingProduct();
            return Ok(products);
        }

        [HttpGet]
        [Route("/GetHighPurchsase")]
        public async Task<IActionResult> GetHighPurchased()
        {
            List<string> customers = new List<string>();
            customers=await _ControllerService.GetHighPurchased();
            return Ok(customers);
        }

        [HttpGet]
        [Route("/GetAllProductMaxPurchased")]
        public async Task<IActionResult> GetAllProductMaxPurchased()
        {
            List<string> productsNames=await _ControllerService.GetAllProductMaxPurchased();
            return Ok(productsNames);
        }
    }
}
