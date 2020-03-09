using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppJWT.Models
{
    public class User
    {

        public string apiKey { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}