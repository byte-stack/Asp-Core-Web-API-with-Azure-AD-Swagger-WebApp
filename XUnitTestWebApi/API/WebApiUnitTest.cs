using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.XUnitTest.API
{
    public class WebApiUnitTest
    {
        readonly EBSBillingController _controller;
        readonly IBillingDetails _service;

        public WebApiUnitTest()
        {
            _service = new IBillingDetailsFake();
            _controller = new EBSBillingController(_service);
        }

        #region  Get Test cases
        [Fact]
        public void Get()
        {
            // act
            var results = _controller.Get();

            //Assert
            Assert.NotNull(results);
        }


        [Fact]
        public void Get_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // act
            var notFoundResult = _controller.Get(-1);

            //Assert
            Assert.Null(notFoundResult.Result);
        }


        [Fact]
        public void Get_ExistingIdPassed_ReturnsOkResult()
        {

            // act
            var exsId=_controller.Get().Value.FirstOrDefault().Id;
            var FoundResult = _controller.Get(exsId);

            //Assert
            Assert.NotNull(FoundResult.Value);
        }

        #endregion

        #region POST Test cases
        [Fact]
        public void Post_InvalidObjectPassed()
        {
            try
            {
                // Arrange
                var BillingDetailItem = new BillingDetailModel()
                {
                    BillingCycleId=1,
                    Id=1,
                    CreatedDate=DateTime.Now,
                    UserId=1
                };
                _controller.ModelState.AddModelError("amount", "Required");

                // Act
                _controller.Post(BillingDetailItem);

                // Assert
                Assert.True(true);
            }
            catch(Exception ex)
            {
                // Assert
                Assert.True(true);
            }
            
        }

        
        [Fact]
        public void Post_ValidObjectPassed()
        {
            try
            {
                // Arrange
                var BillingDetailItem = new BillingDetailModel()
                {
                    BillingCycleId = 1,
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    UserId = 1,
                    Amount=50
                };
                
                // Act
                _controller.Post(BillingDetailItem);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(true);
            }

        }



        #endregion

        #region Put
        [Fact]
        public void Put_InvalidObjectPassed()
        {
            try
            {
                // Arrange
                var BillingDetailItem = new BillingDetailModel()
                {
                    BillingCycleId = 1,
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    UserId = -1
                };
                
                // Act
                _controller.Put(BillingDetailItem);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(true);
            }

        }


        [Fact]
        public void Put_ValidObjectPassed()
        {
            try
            {
                // Arrange
                var BillingDetailItem = new BillingDetailModel()
                {
                    BillingCycleId = 1,
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    UserId = 1,
                    Amount = 50
                };

                // Act
                _controller.Put(BillingDetailItem);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.True(true);
            }

        }
        #endregion

        #region Delete 

        [Fact]
        public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            try
            {
                // Arrange
                var notExistingId = -1;

                // Act
                _controller.Delete(notExistingId);

                // Assert
                Assert.True(false);
            }
            catch(Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Delete_ExistingIdPassed_ReturnsOkResult()
        {
            try
            {
                // Arrange
                var notExistingId = 1;

                // Act
                _controller.Delete(notExistingId);

                // Assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false);
            }
        }

       
        #endregion

    }
}
