using System;

namespace WebAPI.DataServices.Table
{
    /// <summary>
    /// Billing cycle Db table 
    /// </summary>
    public class BillingCycle
    {
        /// <summary>
        /// Id -Primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Billing start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Billing End date
        /// </summary>
        public DateTime EndDate { get; set; }

    }
}
