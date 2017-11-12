using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class OrderDisplay
    {
        public int OrderID { get; set; }
        public string OrderCustomer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }

        public OrderDisplay()
        {

        }
    }
}