using System;
using WebAPI.Business.Interface;
using WebAPI.Controllers;
using Xunit;

namespace XUnitTestWebApi
{
    public class UnitTest1
    {

        private readonly IBillingDetails IBillingDetails;

        [Fact]
        public void Test1()
        {
            //Arrange
            var controller = new EBSBillingController(IBillingDetails);
            var postId = 2;

            //Act
            //var data = await controller.GetPost(postId);

            //Assert
            //Assert.IsType<OkObjectResult>(data);
        }
    }
}
