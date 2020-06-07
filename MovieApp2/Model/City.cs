using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Model
{
    public class City
    {
        [Key]
        public string Postnummer { get; set; }
        public string Poststed { get; set; }
        public virtual List<Users> Kunder { get; set; }
    }
}
