using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QHat.Models
{
    public enum Status {
        untreated,intransit,deliver
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public System.DateTime OrderDate { get; set; }
        public List<Orderitem> Orderitems { get; set; }
        public ApplicationUser User { get; set; }

    }
}
