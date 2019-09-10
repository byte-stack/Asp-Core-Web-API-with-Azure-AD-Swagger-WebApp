﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.Interface;
using WebAPI.Business.Logic;
using WebAPI.Business.Models;

namespace WebAPI.Controllers
{
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
        public ActionResult<BillingDetailModel> Get(long id)
        {
            return IBillingDetails.Get(id);
        }

        /// <summary>
        /// Post the billing object
        /// </summary>
        /// <param name="billingDetailModel">Billing details model </param>
        [HttpPost]
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
        public void Put(long id, BillingDetailModel billingDetailModel)
        {
            IBillingDetails.Put(id, billingDetailModel);
        }

        /// <summary>
        /// Delete the record of billing details
        /// </summary>
        /// <param name="id">Billing details id</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            IBillingDetails.Delete(id);
        }
    }
}