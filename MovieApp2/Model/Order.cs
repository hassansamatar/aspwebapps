using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieApp.Model
{
    public class Order
    {
        public int Id { get; set; }

        public virtual Users OrderUser { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

    }
}