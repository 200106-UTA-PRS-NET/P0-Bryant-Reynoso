using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain
{
    public class Order
    {
        public List<Pizza> pizzasInOrder = new List<Pizza>();
        public List<string> pizzaNames = new List<string>();
        public int storeId { get; }
        public int userId {get;}
        public decimal orderTotal { get; set; }
        public const int pizzaLimit = 100;
        public const decimal totalLimit = 250;
        public decimal total { get; set; }

        public Order(int storeId, int userId)
        {
            this.storeId = storeId;
            this.userId = userId;
        }
        
        public Pizza PremadePizza(KeyValuePair<string, decimal> panSize, KeyValuePair<string,decimal> crustType, string pizzaName) {
            Pizza pizza = new Pizza();
            pizza.setPanSize(panSize.Key,panSize.Value);
            pizza.setCrustType(crustType.Key, crustType.Value);

            //query to get the id of pizza where name = pizzaName
            //use the id to query for all toppings where id = pizzaId
            //use those id's to get all the topping names
            //add those topping names/price

            //pizza.AddTopping(topping.name, topping.price);

            return pizza; //this method will be called this way AddPizza(PremadePizza(kv pan,kv crust,pname))
        }
        
        public void AddPizza(Pizza pizza)
        {
            if (pizzasInOrder.Count < pizzaLimit )
            {
                pizzasInOrder.Add(pizza);
            }
            else
            {
                Console.WriteLine("You have reached the max amount of pizzas");
            }
        }

        public void RemovePizza(Pizza pizza)
        {
            //potentially could remove identical pizzas?
            if (pizzasInOrder.Contains(pizza))
            {
                pizzasInOrder.Remove(pizza);
            }
            else
            {
                Console.WriteLine("Pizza not recognized");
            }
        }

        public decimal CalculateTotal()
        {
            foreach (var pizza in pizzasInOrder)
            {
                total += pizza.CalculateTotal();
            } 
            return total;
        }

    }
}
