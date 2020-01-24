using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Stores
    {
        public Stores()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string StoreAddress { get; set; }
        public string Inventory { get; set; }
        public decimal Sales { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
