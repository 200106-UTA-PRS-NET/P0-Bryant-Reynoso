using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Toppings
    {
        public Toppings()
        {
            OurPizzaToppings = new HashSet<OurPizzaToppings>();
        }

        public int Id { get; set; }
        public string ToppingName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<OurPizzaToppings> OurPizzaToppings { get; set; }
    }
}
