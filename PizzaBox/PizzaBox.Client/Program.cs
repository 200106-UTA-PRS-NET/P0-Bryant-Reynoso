using System;
using System.Collections.Generic;
using PizzaBox.Domain;
using PizzaBox.Domain.Models;
using System.Linq;
using System.Threading;

namespace PizzaBox.Client
{
    //public delegate void Del();
    class Program
    { 
        static void Main(string[] args)
        {
            //PizzaDbContext db = new PizzaDbContext();
            OrderPizza orderPizza = new OrderPizza();
            
            orderPizza.ChooseSigning();
            //orderPizza.ViewUserOrders(3);
            

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("press any key to exit");

            Console.ReadKey();
        } 
    }
}
