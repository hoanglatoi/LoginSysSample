using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiSysSvr.Data
{
    
    public class MyIdentifyUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Comment { get; set; }
    }
}
