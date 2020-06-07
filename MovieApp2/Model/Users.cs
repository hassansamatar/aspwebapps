using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Model
{
    public class Users
    {
        public int Id { get; set; }

        public string Fornavn { get; set; }

        public string Etternavn { get; set; }

        public string Adresse { get; set; }

        public string Epost { get; set; }

        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }

        public virtual City Poststeder { get; set; }

        public virtual List<Order> Orders { get; set; }
        public string AccessLevel { get; set; }

    }
}
