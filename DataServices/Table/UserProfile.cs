using System;

namespace WebAPI.DataServices.Table
{
    /// <summary>
    /// User profile tb table
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Id primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// user id a reference key for the user details record
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// user location /city
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// User address
        /// </summary>
        public string Address { get; set; }
    }
}
