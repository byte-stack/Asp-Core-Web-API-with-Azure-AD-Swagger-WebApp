using System.Collections.Generic;
using WebAPI.DataServices.Table;

namespace WebAPI.DataServices.Contracts
{
    /// <summary>
    /// Billing Repository implementation for get/put/delete/post
    /// </summary>
    public interface IBillingRepository
    {
        /// <summary>
        /// Get the billing details
        /// </summary>
        /// <returns>IEnumerable list of billing table objects </returns>
        IEnumerable<BillingDetails> Get();

        /// <summary>
        /// Get the billing details for the given billing id
        /// </summary>
        /// <param name="id">billing id</param>
        /// <returns>returns billing class object</returns>
        BillingDetails Get(long id);

        /// <summary>
        /// Saves the billing details 
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        void Save(BillingDetails billingDetails);


        /// <summary>
        /// Update the billing details
        /// </summary>
        /// <param name="Id">Billing Id</param>
        /// <param name="billingDetails">billing class object model</param>
        void Put(BillingDetails billingDetails);

        /// <summary>
        /// Remove the billing details from the system
        /// </summary>
        /// <param name="id">billing details id</param>
        void Delete(long id);

    }
}
