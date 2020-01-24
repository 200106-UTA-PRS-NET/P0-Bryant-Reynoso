using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string Pizzas { get; set; }
        public DateTime TimeOrdered { get; set; }
        public decimal Total { get; set; }
        public int Userid { get; set; }
        public int Storeid { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
    }
}
