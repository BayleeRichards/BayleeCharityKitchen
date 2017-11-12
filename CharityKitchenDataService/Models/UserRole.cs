using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class UserRole
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int UserID { get; set; }
    }
}