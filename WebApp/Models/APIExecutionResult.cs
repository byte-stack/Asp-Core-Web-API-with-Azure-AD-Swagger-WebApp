using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.Models
{
    /// <summary>
    /// Model class to process the result from the api at the view
    /// </summary>
    public class APIExecutionResult
    {
        public string GetResult { get; set; }
        public string GetByIdResult { get; set; }
        public string PostResult { get; set; }
        public string PutResult { get; set; }
        public string DeleteResult { get; set; }
    
    }
}
