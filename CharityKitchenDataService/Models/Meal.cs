using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }

        public Meal()
        {

        }
    }
}