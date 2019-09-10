using System;

/// <summary>
/// User table 
/// </summary>
namespace WebAPI.DataServices.Table
{
    /// <summary>
    /// User table 
    /// </summary>
    public class Users
    {
        /// <summary>
        /// User Id -primary key
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        ///  user first name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User Ad detail Id
        /// </summary>
        public string AdUserId { get; set; }

        /// <summary>
        ///  record created date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// User active/deactivate status 
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
