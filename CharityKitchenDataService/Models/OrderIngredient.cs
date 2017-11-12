using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class OrderIngredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }

        public OrderIngredient()
        {

        }
    }
}