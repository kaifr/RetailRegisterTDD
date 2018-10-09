using System.Collections.Generic;

namespace RetailSystem.DAL
{
    internal interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}
