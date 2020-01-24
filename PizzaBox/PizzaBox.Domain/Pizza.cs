using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain
{
    public class Pizza
    {
        public KeyValuePair<string, decimal> panSize { get; set; }
        public KeyValuePair<string,decimal> crustType { get; set; }
        public Dictionary<string, decimal> toppingsOnPizza = new Dictionary<string, decimal>();
        public string pizzaName { get; set; }
        public decimal pizzaNameCost { get; set; }
        public decimal total { get; set; }
        public const int toppingLimit = 3;
  
        public void setPanSize(string pan, decimal panCost)
        {
            panSize = new KeyValuePair<string, decimal>(pan, panCost);
        }

        public void setCrustType(string crustName, decimal crustCost)
        {
            crustType = new KeyValuePair<string, decimal>(crustName, crustCost);
        }

       // public void 

        public void AddTopping(string toppingName, decimal toppingCost)
        {
            if (toppingsOnPizza.Count < toppingLimit)
            {
                toppingsOnPizza.Add(toppingName, toppingCost);
            }
            else
            {
                Console.WriteLine("Pizza has max amount of toppings");
            }
        }

        public void RemoveTopping(string toppingName)
        {
            if (toppingsOnPizza.ContainsKey(toppingName))
            {
                toppingsOnPizza.Remove(toppingName);
            }
            else
            {
                Console.WriteLine("Topping not recognized");
            }
        }

        public decimal CalculateTotal()
        {
            //decimal toppingTotal = 0;

            ////add all toppings on pizza together
            //foreach (var topping in toppingsOnPizza)
            //{
            //    toppingTotal += topping.Value;
            //}

            //total = panSize.Value + crustType.Value + toppingTotal;
            total = panSize.Value + crustType.Value + pizzaNameCost;

            return total;
        }
         
    }
}
