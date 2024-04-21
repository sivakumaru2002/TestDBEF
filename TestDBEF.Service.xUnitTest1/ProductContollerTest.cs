using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Logging;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBEF.Controllers;
using TestDBEF.Repository;
using TestDBEF.Repository.Entity;
using TestDBEF.Repository.Implementation;
using TestDBEF.Repository.Interface;
using TestDBEF.Service.Implementation;
using TestDBEF.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestDBEF.Service.xUnitTest1
{
    public class ProductContollerTest
    {
        private readonly Mock<IControllerService> _controllerService = new();
        public ProductContollerTest()
        {
            _controllerService = new Mock<IControllerService>();
        }
        [Fact]
        public void CheckAllProductController()
        {
            _controllerService.Setup(r => r.GetAllProduct()).ReturnsAsync(new List<Product>());
            var allController = new AllController(_controllerService.Object);
            var okObjectResult = allController.GetAllProduct();
            OkObjectResult  okresult = okObjectResult.Result as OkObjectResult;
            Assert.IsType<OkObjectResult>(okresult);
        }
       
        [Theory]
        [InlineData("Product V", 20, 300)]
        public void CreateProductDoneOrNo(string name, int quantity, int price)
        {
            Product product = new Product();
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            _controllerService.Setup(x=>x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(product);
            var allController = new AllController(_controllerService.Object);
            var productdata = allController.CreateProduct(product);
            Assert.IsType<OkObjectResult>(productdata.Result as OkObjectResult);
        }
             
        [Fact]
        public async void CheckAllProductID()
        {
            Guid guid = Guid.NewGuid();
            _controllerService.Setup(x => x.GetProductByID(It.IsAny<Guid>())).ReturnsAsync(new Product());
            var allController = new AllController(_controllerService.Object);
            var productdata = await allController.GetProductbyId(guid);
            Assert.IsType<OkObjectResult>(productdata as OkObjectResult);
        }
        [Fact]
        public async void CheckAllProductID2()
        {
            Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            _controllerService.Setup(x => x.GetProductByID(It.IsAny<Guid>())).ReturnsAsync(new Product());
            var allController = new AllController(_controllerService.Object);
            var productdata =await allController.GetProductbyId(guid);
            Assert.IsType<NotFoundObjectResult>(productdata);
        }
        [Fact]
        public void DeleteProductByID()
        {
            Guid guid = Guid.NewGuid();
            _controllerService.Setup(x => x.DeleteProduct(It.IsAny<Guid>())).ReturnsAsync(new Product());
            var allController = new AllController(_controllerService.Object);
            var productdata = allController.DeleteProduct(guid);
            Assert.IsType<OkObjectResult>(productdata.Result as OkObjectResult);
        }

        [Theory]
        [InlineData("Product V", 20, 300)]
        public void CheckUpdateProductController(string name, int quantity, int price)
        {
            Product product = new Product();
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            _controllerService.Setup(x => x.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(product);
            var allController = new AllController(_controllerService.Object);
            var productdata = allController.UpdateProduct(product);
            Assert.IsType<OkObjectResult>(productdata.Result as OkObjectResult);
        }

    }
}
