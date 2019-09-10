using System;

namespace WebAPI.DataServices.Table
{
    /// <summary>
    ///  Billing details table
    /// </summary>
    public class BillingDetails
    {
        /// <summary>
        /// Id- primary key for the table
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// user id a reference key for the user billing record
        /// </summary>
        public long UserId { get; set; } = 1;

        /// <summary>
        /// Billing id a reference key for the billing cycle id
        /// </summary>
        public long BillingCycleId { get; set; } = 1;

        /// <summary>
        /// Amount for the billing period 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// record created date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
