using WebAPI.DataServices.Contracts;
using WebAPI.DataServices.Data;
using WebAPI.DataServices.Table;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.DataServices.Repositories
{
    /// <summary>
    /// user repository to implement user level functions
    /// </summary>
    public class UserRepository: IUserRepository
    {
        /// <summary>
        /// EBS data context
        /// </summary>
        private readonly EBSContext _context;

        /// <summary>
        /// default constructor for the repo
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(EBSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all the users from the db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> Get()
        {
            return _context.User;
        }


    }
}
