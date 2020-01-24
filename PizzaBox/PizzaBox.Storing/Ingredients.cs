using System;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
    public static class Ingredients
    {
        public static Dictionary<string, decimal> crust = new Dictionary<string, decimal>();
        public static Dictionary<string, decimal> size = new Dictionary<string, decimal>();
        public static Dictionary<string, decimal> toppings = new Dictionary<string, decimal>();
    }
}
