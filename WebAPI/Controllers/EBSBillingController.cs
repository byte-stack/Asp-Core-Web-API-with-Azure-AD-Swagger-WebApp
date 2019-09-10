using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;

namespace WebAPI.Controllers
{

    /// <summary>
    /// EBS controller for implementation the basic operation of web api
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EBSBillingController : ControllerBase
    {
        /// <summary>
        /// Billing interface dependency injection 
        /// </summary>
        private readonly IBillingDetails IBillingDetails;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="iBillingDetails"></param>
        public EBSBillingController(IBillingDetails iBillingDetails)
        {
            IBillingDetails = iBillingDetails;
        }

        /// <summary>
        /// get the all billing details list
        /// </summary>
        /// <returns>returns list of billing objects</returns>
        [HttpGet]
        [EnableCors("AllowSpecificOrigins")]
        public ActionResult<IEnumerable<BillingDetailModel>> Get()
        {
            return IBillingDetails.Get().ToList();
        }

        /// <summary>
        /// Get the billing details for the specific billing id
        /// </summary>
        /// <param name="id">Billing details Id</param>
        /// <returns>returns list of billing objects</returns>
        [HttpGet("{id}")]
        [EnableCors("AllowSpecificOrigins")]
        public ActionResult<BillingDetailModel> Get(long id)
        {
            return IBillingDetails.Get(id);
        }

        /// <summary>
        /// Post the billing object
        /// </summary>
        /// <param name="billingDetailModel">Billing details model </param>
        [HttpPost]
        [EnableCors("AllowSpecificOrigins")]
        public void Post(BillingDetailModel billingDetailModel)
        {
            IBillingDetails.Post(billingDetailModel);
        }

        /// <summary>
        /// Update the billing details 
        /// </summary>
        /// <param name="id">Billing details id</param>
        /// <param name="billingDetailModel">Billing details model</param>
        [HttpPut("{id}")]
        [EnableCors("AllowSpecificOrigins")]
        public void Put(BillingDetailModel billingDetailModel)
        {
            IBillingDetails.Put(billingDetailModel);
        }

        /// <summary>
        /// Delete the record of billing details
        /// </summary>
        /// <param name="id">Billing details id</param>
        [HttpDelete("{id}")]
        [EnableCors("AllowSpecificOrigins")]
        public void Delete(long id)
        {
            IBillingDetails.Delete(id);
        }
    }
}
