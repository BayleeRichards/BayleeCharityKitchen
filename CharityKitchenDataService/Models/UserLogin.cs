using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }

        public UserLogin()
        {

        }
    }
}