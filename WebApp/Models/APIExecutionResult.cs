using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.Models
{
    public class APIExecutionResult
    {
        public string GetResult { get; set; }
        public string GetByIdResult { get; set; }
        public string PostResult { get; set; }
        public string PutResult { get; set; }
        public string DeleteResult { get; set; }
    
    }
}
