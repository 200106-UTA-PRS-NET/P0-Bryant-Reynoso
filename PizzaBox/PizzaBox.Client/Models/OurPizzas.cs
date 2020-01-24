using System;
using System.Collections.Generic;

namespace PizzaBox.Client.Models
{
    public partial class OurPizzas
    {
        public OurPizzas()
        {
            OurPizzaToppings = new HashSet<OurPizzaToppings>();
        }

        public int Id { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<OurPizzaToppings> OurPizzaToppings { get; set; }
    }
}
