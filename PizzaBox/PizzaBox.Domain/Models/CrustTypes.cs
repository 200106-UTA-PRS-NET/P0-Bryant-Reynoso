using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class CrustTypes
    {
        public int Id { get; set; }
        public string CrustName { get; set; }
        public decimal Price { get; set; }
    }
}
