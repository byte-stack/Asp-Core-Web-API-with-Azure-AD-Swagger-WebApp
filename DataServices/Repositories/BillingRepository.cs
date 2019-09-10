using WebAPI.DataServices.Contracts;
using WebAPI.DataServices.Data;
using WebAPI.DataServices.Table;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.DataServices.Repositories
{

    /// <summary>
    /// Billing Repository implementation for get/put/delete/post
    /// </summary>
    public class BillingRepository: IBillingRepository
    {

        /// <summary>
        /// EBS data context
        /// </summary>
        private readonly EBSContext _context;

        /// <summary>
        /// default constructor for the repo
        /// </summary>
        /// <param name="context"></param>
        public BillingRepository(EBSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the billing details
        /// </summary>
        /// <returns>IEnumerable list of billing table objects </returns>
        public IEnumerable<BillingDetails> Get()
        {
            return _context.BillingDetails;
        }

        /// <summary>
        /// Get the billing details for the given billing id
        /// </summary>
        /// <param name="id">billing id</param>
        /// <returns>returns billing class object</returns>
        public BillingDetails Get(long id)
        {
            return _context.BillingDetails.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Saves the billing details 
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        public void Save(BillingDetails billingDetails)
        {
            _context.BillingDetails.Add(billingDetails);
            _context.SaveChanges();
        }


        /// <summary>
        /// update the billing details 
        /// </summary>
        /// <param name="billingDetails">billing class object model</param>
        public void Put(BillingDetails billingDetails)
        {
            _context.BillingDetails.Update(billingDetails);
            _context.SaveChanges();
        }
        /// <summary>
        /// Remove the billing details from the system
        /// </summary>
        /// <param name="id">billing details id</param>
        public void Delete(long id)
        {
            _context.BillingDetails.Remove(_context.BillingDetails.Where(x=>x.Id==id).FirstOrDefault());
            _context.SaveChanges();
        }



    }
}
