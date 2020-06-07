using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieApp.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; } 
        public string Actor { get; set; }
        public string Director { get; set; }
        public int ReleasedYear { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string ErrorMessage { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}