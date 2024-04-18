using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBEF.Repository.Entity;
using TestDBEF.Repository.Interface;

namespace TestDBEF.Service.xUnitTest1
{
    public class ProductRepoTest
    {
        private readonly Mock<IControllerRepository> _ControllerRepo = new();
        public ProductRepoTest() { 
            _ControllerRepo = new Mock<IControllerRepository>();
        }
        [Theory]
        [InlineData("Product V", 20, 300)]
        public async void CreateProductRepo(string name, int quantity, int price)
        {
            Product product = new Product();
            product.product_id = Guid.NewGuid();
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            _ControllerRepo.Setup(x => x.CreateProduct(product)).ReturnsAsync(new Product());
            Product productdata = await _ControllerRepo.Object.CreateProduct(product);
            Assert.NotNull(productdata);
        }
        [Fact]
        public async void CheckHighSellingProduct()
        {
            _ControllerRepo.Setup(x => x.GetHighSellingProduct()).ReturnsAsync(new List<string>());
            List<string> productdata = await _ControllerRepo.Object.GetHighSellingProduct();
            Assert.IsType<List<string>>(productdata);
        }
    }
}
