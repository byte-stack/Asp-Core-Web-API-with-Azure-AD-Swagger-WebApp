using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;

namespace WebAPI.XUnitTest.API
{
    class IBillingDetailsFake : IBillingDetails
    {
        /// <summary>
        /// a read only list of BillingDetailModel entities.
        /// </summary>
        private readonly List<BillingDetailModel> _billingDetails;


        /// <summary>
        /// Default constructor 
        /// </summary>
        public IBillingDetailsFake()
        {
            _billingDetails = new List<BillingDetailModel>()
            {
                new BillingDetailModel() { Id = 1,Amount=10,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
                new BillingDetailModel()  { Id = 2,Amount=20,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
                new BillingDetailModel()  { Id = 3,Amount=30,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
            };
        }


        /// <summary>
        /// Remove the billing details from the system
        /// </summary>
        /// <param name="id">billing details id</param>
        public void Delete(long id)
        {
            var existing = _billingDetails.Where(a => a.Id == id).FirstOrDefault();
            _billingDetails.Remove(existing);
        }

        /// <summary>
        /// Get the billing details
        /// </summary>
        /// <returns>IEnumerable list of billing table objects </returns>
        public IEnumerable<BillingDetailModel> Get()
        {
            return _billingDetails;
        }

        /// <summary>
        /// Get the billing details for the given billing id
        /// </summary>
        /// <param name="id">billing id</param>
        /// <returns>returns billing class object</returns>
        public BillingDetailModel Get(long id)
        {
            return _billingDetails.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Saves the billing details 
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        public void Post(BillingDetailModel billingDetails)
        {
            // check if the amount is not given in the model.
            if(billingDetails.Amount==0)
            {
                throw new Exception("Invalid Model state.");
            }
            _billingDetails.Add(billingDetails);
        }

        /// <summary>
        /// Update the billing details
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        public void Put(BillingDetailModel billingDetails)
        {
            //check of the given id of billing is not present in the mock data 
            var existing = _billingDetails.Where(a => a.Id == billingDetails.Id).FirstOrDefault();
            if (existing==null)
            {
                throw new Exception("Invalid Model state.");
            }
            existing.Amount = billingDetails.Amount;
        }
    }
}
