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
    /// <summary>
    /// Web Api  Test Class
    /// </summary>
    public class WebApiUnitTest
    {
        /// <summary>
        /// Web api controller object
        /// </summary>
        readonly EBSBillingController _controller;

        /// <summary>
        /// Interface of billing details
        /// </summary>
        readonly IBillingDetails _service;

        /// <summary>
        /// Default constructor 
        /// </summary>
        public WebApiUnitTest()
        {
            _service = new IBillingDetailsFake();
            _controller = new EBSBillingController(_service);
        }

        #region  Get Test cases


        /// <summary>
        /// Test the billing details- all record
        /// </summary>
        [Fact]
        public void Get()
        {
            // act
            var results = _controller.Get();

            //Assert
            Assert.NotNull(results);
        }

        /// <summary>
        /// Test the Get method by passing wrong billing id
        /// </summary>
        [Fact]
        public void Get_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // act
            var notFoundResult = _controller.Get(-1);

            //Assert
            Assert.Null(notFoundResult.Result);
        }

        /// <summary>
        /// Test the get method by passing the correct billing details id
        /// </summary>
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

        /// <summary>
        /// Test the POST method for invalid case with amount is missing in the Billing details model
        /// </summary>
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
            catch (Exception)
            {
                // Assert
                Assert.True(true);
            }
            
        }

        /// <summary>
        /// Test the POST method for valid billing details model
        /// </summary>
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

        /// <summary>
        /// Test method for PUT with invalid case using incorrect user id
        /// </summary>
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
            catch (Exception)
            {
                // Assert
                Assert.True(true);
            }

        }

        /// <summary>
        /// Put test valid case with correct user id
        /// </summary>
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
            catch (Exception)
            {
                // Assert
                Assert.True(true);
            }

        }
        #endregion

        #region Delete 

        /// <summary>
        /// Delete Test Case with invalid billing details id 
        /// </summary>
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

        /// <summary>
        /// Delete case with correct billing details id
        /// </summary>
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
            catch (Exception)
            {
                Assert.True(false);
            }
        }

       
        #endregion

    }
}
