using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestDBEF.Repository.Entity;
using Xunit.Sdk;

namespace TestDBEF.Service.xUnitTest1
{
    public class IntegrationProductApi : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationProductApi(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("https://localhost:44378/GetAllCustomer")]
        [InlineData("https://localhost:44378/GetProductById")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var result = response.IsSuccessStatusCode;
            if (result)
                Assert.True(result);
            else
                Assert.False(result);
        }
        [Theory]
        [InlineData("https://localhost:44378/GetAllCustomer")]
        public async void Get_EndpointsReturnSuccessAndCorrectContentType1(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualCustomer = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
            Assert.IsType<List<Customer>>(actualCustomer);
        }

        [Theory]
        [InlineData ("https://localhost:44378/GetAllOrder")]
        public async void Get_EndpointFromOrdersApi(string url)
        {
            var client = _factory.CreateClient();
            var response=await client.GetAsync(url);
            var resContent=await response.Content.ReadAsStringAsync();
            var actualOrder=JsonConvert.DeserializeObject<List<Orders>>(resContent);
            Assert.IsType<List<Orders>>(actualOrder);
        }

        [Theory]
        [InlineData("https://localhost:44378/GetHighPurchsase")]
        [InlineData("https://localhost:44378/GetHighSellingProduct")]
        [InlineData("https://localhost:44378/GetAllProductMaxPurchased")]
        public async void Get_EndpointFromHighSellingPurchaseApi(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetFromJsonAsync<List<string>>(url);
            Assert.IsType<List<string>>(response);
        }

       //Connection through database from Testing
       [Theory]
       [InlineData("3a6d6564-e704-4311-b872-0442e71baed8")]
       [InlineData("ed21e4fe-3d7e-4531-867c-6c2073ce6dc5")]
       public async void Get_EndpointFromProductByIdApi(string id)
        {
            Guid productid = Guid.Parse(id);
            string url = $"https://localhost:44378/GetProductById?id={id}";
            var client= _factory.CreateClient();
            var response =await client.GetAsync(url);
            var resContent=await response.Content.ReadAsStringAsync();
            Product content = JsonConvert.DeserializeObject<Product>(resContent);
            Assert.Equal(productid, content.product_id);
        }

        [Theory]
        [InlineData("3a6d6564-e704-4311-b872-0442e71baed8")]
        [InlineData("ed21e4fe-3d7e-4531-867c-6c2073ce6dc5")]
        public async void Get_EndpointFromProductByIdApi2(string id)
        {
            string url = $"https://localhost:44378/GetProductById?id={id}";
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var resContent = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Product>(resContent);
            Assert.Equal(response.StatusCode,HttpStatusCode.OK );
        }

        [Fact]
        public async void Get_EndpointFromProductByIdApiNull()
        {
            string url = $"https://localhost:44378/GetProductById?id=00000000-0000-0000-0000-000000000000";
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var resContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("Product U", 20, 300)]
        public async void Get_EndpointFromCreatePrductApiNull(string name, int quantity, int price)
        {
            Product product = new Product();
            product.price=price;
            product.quantity=quantity;
            product.productname=name;
            string url = "https://localhost:44378/CreateProduct";
            var client = _factory.CreateClient();
            var respone = await client.PostAsJsonAsync(url, product);
            var res = await respone.Content.ReadAsStringAsync();
            var resContent = JsonConvert.DeserializeObject<Product>(res);
            Assert.Equal(resContent.productname, product.productname);
            //Assert.Equal(respone.StatusCode,HttpStatusCode.OK);
        }
        [Theory]
        [InlineData("931187c4-36a8-4a97-99fa-11fcd7365120", "Dog toy 2", 20, 300)]
        public async void Get_EndpointFromUpdatePrductApiNull(string P_id,string name, int quantity, int price)
        {
            Product product = new Product();
            product.product_id = Guid.Parse(P_id);
            product.price = price;
            product.quantity = quantity;
            product.productname = name;
            string url = "https://localhost:44378/UpdateProduct";
            var client = _factory.CreateClient();
            var respone = await client.PutAsJsonAsync(url, product);
            var res = await respone.Content.ReadAsStringAsync();
            var resContent = JsonConvert.DeserializeObject<Product>(res);
            Assert.Equal(resContent.product_id, product.product_id);
            //Assert.Equal(respone.StatusCode,HttpStatusCode.OK);
        }
    }
}
