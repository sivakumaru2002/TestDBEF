using Microsoft.AspNetCore.Mvc;
using TestDBEF.Controllers;
using TestDBEF.Service.Interface;

namespace TestDBEF.Service.xUnitTest1
{
    public class UnitTest1
    {
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