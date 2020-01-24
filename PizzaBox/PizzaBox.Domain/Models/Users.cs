using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public bool IsEmployee { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
