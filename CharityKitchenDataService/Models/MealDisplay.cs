using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class MealDisplay
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public decimal Price { get; set; }

        public MealDisplay()
        {

        }
    }
}