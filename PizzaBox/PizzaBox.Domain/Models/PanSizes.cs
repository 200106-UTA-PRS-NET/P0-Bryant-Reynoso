using System;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public partial class PanSizes
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}
