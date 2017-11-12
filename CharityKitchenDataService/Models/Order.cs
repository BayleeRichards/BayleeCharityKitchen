using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderCustomer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}