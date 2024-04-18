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

        private readonly IControllerService _ControllerService;

        public AllController(IControllerService controllerService)
        {

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
        [Route("/GetProductById")]
        public async Task<IActionResult> GetProductbyId(Guid id)
        {
            if(id.ToString()== "00000000-0000-0000-0000-000000000000") return NotFound(id);
            var product= await _ControllerService.GetProductByID(id);
            if (product == null) { return NotFound(id); }
            return Ok(product);
        }
        [HttpPost]
        [Route ("/CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
           Product products= await _ControllerService.CreateProduct(product);
            if (products == null) return BadRequest(products);
            return Ok(products);
        }

        [HttpDelete]
        [Route("/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid productid)
        {
            Product product =await  _ControllerService.DeleteProduct(productid);
            if (product == null) return BadRequest(productid);
            return Ok(product);
        }
        [HttpPut]
        [Route("/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            Product product1 = await _ControllerService.UpdateProduct(product);
            if(product1 == null) return BadRequest(product);
            return Ok(product1);
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
