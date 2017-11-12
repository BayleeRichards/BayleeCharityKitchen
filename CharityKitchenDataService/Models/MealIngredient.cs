﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharityKitchenDataService.Models
{
    public class MealIngredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        public int MealID { get; set; }

        public MealIngredient()
        {

        }
    }
}