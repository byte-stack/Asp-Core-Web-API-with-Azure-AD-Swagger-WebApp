using System.Collections.Generic;
using System.Linq;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;
using WebAPI.DataServices.Contracts;

namespace WebAPI.Business.Logic
{
    /// <summary>
    /// Billing Details Business layer implementation.
    /// </summary>
    public class BillingDetails : IBillingDetails
    {
        /// <summary>
        /// interface for the Billing repo
        /// </summary>
        private readonly IBillingRepository iBillingRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billingRepository"></param>
        public BillingDetails(IBillingRepository billingRepository)
        {
            iBillingRepository = billingRepository;
        }

        /// <summary>
        /// Get the billing details
        /// </summary>
        /// <returns>IEnumerable list of billing table objects </returns>
        public IEnumerable<BillingDetailModel> Get()
        {
            return iBillingRepository.Get().Select(x => new BillingDetailModel {
                Amount=x.Amount,
                BillingCycleId=x.BillingCycleId,
                CreatedDate=x.CreatedDate,
                UserId=x.UserId,
                Id=x.Id
            }).ToList();
        }

        /// <summary>
        /// Get the billing details for the given billing id
        /// </summary>
        /// <param name="id">billing id</param>
        /// <returns>returns billing class object</returns>
        public BillingDetailModel Get(long id)
        {
            var DbBillingDetails = iBillingRepository.Get(id);
            return new BillingDetailModel
            {
                Amount = DbBillingDetails.Amount,
                BillingCycleId = DbBillingDetails.BillingCycleId,
                CreatedDate = DbBillingDetails.CreatedDate,
                UserId = DbBillingDetails.UserId,
                Id = DbBillingDetails.Id
            };
        }


        /// <summary>
        /// Saves the billing details 
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        public void Post(BillingDetailModel billingDetails)
        {
            iBillingRepository.Save(new DataServices.Table.BillingDetails {
                Amount = billingDetails.Amount,
                BillingCycleId = billingDetails.BillingCycleId,
                CreatedDate = billingDetails.CreatedDate,
                UserId = billingDetails.UserId,
            });
        }

        /// <summary>
        /// Update the billing details
        /// </summary>
        /// <param name="Id">Billing Id</param>
        /// <param name="billingDetails">billing class object model</param>
        public void Put(BillingDetailModel billingDetails)
        {
            iBillingRepository.Put(new DataServices.Table.BillingDetails
            {
                Amount = billingDetails.Amount,
                BillingCycleId = billingDetails.BillingCycleId,
                CreatedDate = billingDetails.CreatedDate,
                UserId = billingDetails.UserId,
                Id= billingDetails.Id
            });
        }

        /// <summary>
        /// Remove the billing details from the system
        /// </summary>
        /// <param name="id">billing details id</param>
        public void Delete(long id)
        {
            iBillingRepository.Delete(id);
        }

    }
}
