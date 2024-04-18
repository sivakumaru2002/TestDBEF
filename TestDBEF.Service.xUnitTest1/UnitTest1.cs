using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TestDBEF.Controllers;
using TestDBEF.Service.Interface;

namespace TestDBEF.Service.xUnitTest1
{
    public class UnitTest1: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("https://localhost:44378/api/GetAllCustomer")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var result = response.IsSuccessStatusCode; 
            Assert.False(result);
        }

       
        [Fact]
        public void Test1()
        {

            Assert.Equal(7, Add(3,4));
        }
        [Fact]
        public void FailinTestCases()
        {
            Assert.Equal(Add(2, 2), 5);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,2,3)]
        [InlineData(3,3,6)]
        public void TestMultipleInput(int x,int y,int expect)
        {
            Assert.Equal(expect, Add(x, y));
        }
        int Add(int x,int y)
        {
            return x + y;
        }

        [Theory]
        [InlineData("Siva","siva")]
        [InlineData("Mug","Mug")]
        public void TestMulitpleName(string name1,string name2)
        {
            Assert.Equal(name1, name2);
        }
     
    }
}