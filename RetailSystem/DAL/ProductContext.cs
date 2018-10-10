using System.Collections.Generic;
using System.Data.Entity;

namespace RetailSystem.DAL
{
    public class ProductContext : DbContext
    {

        public virtual IEnumerable<DAL.Product> Products { get; set; }

        public class Product
        {
            public int BlogId { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }

            public virtual List<Product> Products { get; set; }
        }
    }
}