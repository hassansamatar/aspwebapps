using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieApp.Model
{
    public class OrderItem
    {
        public int Id { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Order Order { get; set; }
    }
}