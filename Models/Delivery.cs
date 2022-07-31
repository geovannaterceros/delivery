
using System.Collections.Generic;

namespace WebApiDelivery.Models
{
    public class Delivery
    {
        public int Sku{ get; set; }

        public string Provider { get; set; }

        public List<Product> Products { get; set; }

        public string Offer { get; set; }
    }
}
