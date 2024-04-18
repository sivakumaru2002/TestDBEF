using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBEF.Repository.Entity;
using TestDBEF.Repository.Interface;
using TestDBEF.Service.Interface;

namespace TestDBEF.Service.xUnitTest1
{
    public class ProductServiceTest
    {
        private readonly Mock<IControllerService> _controllerService = new();
        public ProductServiceTest() {
            _controllerService = new Mock<IControllerService>();
        }
        [Fact]
        public async void CheckAllProductService()
        {
            _controllerService.Setup(p => p.GetAllProduct()).ReturnsAsync(new List<Product>());
            var result = await _controllerService.Object.GetAllProduct();
            Assert.IsType<List<Product>>(result);
        }

        [Theory]
        [InlineData("Product V", 20, 300)]
        public async void CreateProductService(string name, int quantity, int price)
        {
            Product product = new Product();
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            _controllerService.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);
            var productdata = await _controllerService.Object.CreateProduct(product);
            Assert.IsType<Product>(productdata as Product);
        }

        [Fact]
        public void CreateProductByIdService()
        {
            Guid userid = Guid.NewGuid();
            _controllerService.Setup(x => x.GetProductByID(It.IsAny<Guid>())).ReturnsAsync(new Product());
            var productdata = _controllerService.Object.GetProductByID(userid);
            Assert.IsType<Product>(productdata.Result);
        }

        [Fact]
        public async void CreateProductByIdService2()
        {
            Guid userid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var prod = new Product();
            _controllerService.Setup(x => x.GetProductByID(userid)).ReturnsAsync(prod);
            Product productdata =await _controllerService.Object.GetProductByID(userid);
            Assert.Equal(prod, productdata);
        }

        [Fact]
        public void DeleteProductByIDService()
        {
            Guid userid = Guid.NewGuid();
            _controllerService.Setup(x => x.DeleteProduct(It.IsAny<Guid>())).ReturnsAsync(new Product());
            var productdata = _controllerService.Object.DeleteProduct(userid);
            Assert.IsType<Product>(productdata.Result);
        }

        [Theory]
        [InlineData("Product V", 20, 300)]
        public async void UpdateProductService(string name, int quantity, int price)
        {
            Product product = new Product();
            product.product_id = Guid.NewGuid();
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            _controllerService.Setup(x => x.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(product);
            var productdata = await _controllerService.Object.UpdateProduct(product);
            Assert.IsType<Product>(productdata);
        }
    }
}
