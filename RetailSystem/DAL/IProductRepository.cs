using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.DAL
{
    interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}
